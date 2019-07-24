using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileLinesCompare
{
    public class PatchResult
    {
        public Dictionary<string, string> OriginalFilesDetail { get; set; }
        public List<string> ModifiedFiles { get; set; }

        public PatchResult()
        {
            OriginalFilesDetail = new Dictionary<string, string>();
            ModifiedFiles = new List<string>();
        }
    }
}
