using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileLinesCompare
{
    public class FileCompareService
    {
        public string CurrentFile { get; private set; }
        public event EventHandler ReportProgress;

        public string CompareFiles(string ignoreFileLocation, string localWorkingFolderPath, string patchFileName)
        {
            StringBuilder resultsWriter = new StringBuilder();

            try
            {
                PatchResult patchResult = GetFilesToCompareFromPatch(localWorkingFolderPath, patchFileName);

                List<string> ignoreFileList = PopulateIgnoreList(ignoreFileLocation);

                //construct the line dictionaries used for comparing
                foreach (var fileName in patchResult.ModifiedFiles)
                {
                    CurrentFile = fileName;
                    if (ReportProgress != null)
                    {
                        ReportProgress(this, EventArgs.Empty);
                    }

                    //get the line dictionary for the modified file
                    Dictionary<string, int> updatedLineCountDict = PopulateLineDictionary(fileName, ignoreFileList);

                    //get the line dictionary for the previous revision
                    if (!patchResult.OriginalFilesDetail.ContainsKey(fileName))
                    {
                        resultsWriter.AppendLine(string.Format("NEW FILE: Unable to locate previous revision for {0}",
                            fileName) + Environment.NewLine);
                    }
                    else
                    {
                        var revDetail = patchResult.OriginalFilesDetail[fileName];
                        StreamReader sr = GetContentLinesForRevision(fileName, revDetail);
                        Dictionary<string, int> baseLineCountDict = PopulateLineDictionaryFromStream(sr, ignoreFileList);

                        //list out any missing / non matching number of items in base file
                        foreach (var entry in baseLineCountDict)
                        {
                            //found matching item in updated
                            if (!updatedLineCountDict.ContainsKey(entry.Key))
                            {
                                var error = string.Format("{0}: Modified file does not contain line:{1}{2}", fileName, Environment.NewLine,
                                    entry.Key);
                                resultsWriter.AppendLine(error + Environment.NewLine);
                            }
                            else
                            {
                                //count of lines not matching
                                var baseLineCount = entry.Value;
                                var updatedLineCount = updatedLineCountDict[entry.Key];
                                if (baseLineCount != updatedLineCount)
                                {
                                    var error = string.Format(
                                        "{0}: Original file count of {1} does not match modified file count of {2} for line:{3}{4}",
                                        fileName, baseLineCount, updatedLineCount,Environment.NewLine, entry.Key);
                                    resultsWriter.AppendLine(error + Environment.NewLine);
                                }
                            }
                        }

                        //list out any lines in updated file but not base file
                        foreach (var updatedEntry in updatedLineCountDict)
                        {
                            if (!baseLineCountDict.ContainsKey(updatedEntry.Key))
                            {
                                var error = string.Format("{0}: Original file does not contain line:{1}{2}", fileName, Environment.NewLine, updatedEntry.Key);
                                resultsWriter.AppendLine(error + Environment.NewLine);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                resultsWriter.AppendLine(ex.Message);
            }
            finally
            {
                resultsWriter.AppendLine("Completed.");
            }

            return resultsWriter.ToString();
        }

        public string GetPatchForChanges(string localWorkingFolder)
        {
            //Create a patch file to identify which files to include in comparison
            string patchFileName = Path.GetTempFileName();
            Process createPatchProcess = Process.Start("TortoiseProc.exe",
                string.Format("/command:createpatch /path:\"{0}\" /savepath:\"{1}\" /noview",
                    localWorkingFolder, patchFileName));
            createPatchProcess.WaitForExit();

            var patchFileInfo = new FileInfo(patchFileName);
            return patchFileInfo.FullName;
        }

        private Dictionary<string, int> PopulateLineDictionary(string fileName, List<string> linesToIgnore)
        {
            StreamReader file = new StreamReader(fileName);
            return (PopulateLineDictionaryFromStream(file, linesToIgnore));
        }

        private Dictionary<string, int> PopulateLineDictionaryFromStream(StreamReader fileContent, List<String> linesToIgnore)
        {
            var dict = new Dictionary<string, int>();
            try
            {
                string line;
                while ((line = fileContent.ReadLine()) != null)
                {
                    var trimmedCasedLine = line.Trim().ToUpper();
                    //ignore comments
                    if (trimmedCasedLine.StartsWith(Constants.COMMENTS_IDENTIFIER) || trimmedCasedLine.Length == 0)
                    {
                        continue;
                    }

                    //handle ignore lines
                    if (linesToIgnore.Contains(trimmedCasedLine))
                    {
                        continue;
                    }

                    if (!dict.ContainsKey(trimmedCasedLine))
                    {
                        dict.Add(trimmedCasedLine, 1);
                    }
                    else
                    {
                        dict[trimmedCasedLine] += 1;
                    }
                }
            }
            finally
            {
                fileContent.Close();
            }

            return dict;
        }

        private List<string> PopulateIgnoreList(string ignoreFileName)
        {
            var ignoreList = new List<string>();
            StreamReader file = new StreamReader(ignoreFileName);
            try
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    ignoreList.Add(line.Trim().ToUpper());
                }
            }
            finally
            {
                file.Close();
            }

            return ignoreList;
        }

        private StreamReader GetContentLinesForRevision(string fileName, string revision)
        {
            Process processHandler = new Process
            {
                StartInfo =
                {
                    FileName = "svn.exe",
                    Arguments = string.Format("cat {0} -r {1}", fileName, revision),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WorkingDirectory = Path.GetTempPath(),
                    CreateNoWindow = true
                }
            };

            //Start the process
            processHandler.Start();

            //Get program output
            var sr = processHandler.StandardOutput;
            return sr;
        }

        private PatchResult GetFilesToCompareFromPatch(string localWorkingFolder, string patchFileName)
        {
            //get list of files selected in patch:
            var patchResult = new PatchResult();

            var lines = File.ReadLines(patchFileName);
            foreach (string line in lines)
            {
                //Get the list of original files
                if (line.StartsWith(Constants.ORIGINAL_FILE_IDENTIFIER) && !line.Contains(Constants.NOT_EXISTING_FILE_IDENTIFIER))
                {
                    if (line.Contains(Constants.REVISION_DELIMITER.ToString()))
                    {
                        var content = line.Split(Constants.REVISION_DELIMITER);
                        var filename = content[0].Replace(Constants.ORIGINAL_FILE_IDENTIFIER, string.Empty);
                        var rev = content[1].Replace(Constants.REVISION_PREFIX, String.Empty).Replace(Constants.REVISION_SUFFIX,string.Empty);
                        patchResult.OriginalFilesDetail.Add(CreatePathForFile(localWorkingFolder, filename), rev);
                    }
                }

                if (line.StartsWith(Constants.MODIFIED_FILE_IDENTIFIER))
                {
                    if (line.Contains(Constants.REVISION_DELIMITER.ToString()))
                    {
                        var content = line.Split(Constants.REVISION_DELIMITER);
                        patchResult.ModifiedFiles.Add(CreatePathForFile(localWorkingFolder, content[0].Replace(Constants.MODIFIED_FILE_IDENTIFIER, String.Empty)));
                    }
                }
            }

            return patchResult;
        }

        private string CreatePathForFile(string workingRepoPath, string fileName)
        {
            fileName = fileName.Trim();
            fileName = fileName.Replace(@"/", @"\");
            return workingRepoPath + fileName;
        }
    }
}
