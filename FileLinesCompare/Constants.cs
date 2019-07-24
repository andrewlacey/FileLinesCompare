using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FileLinesCompare
{
    public class Constants
    {
        public const string ORIGINAL_FILE_IDENTIFIER = "---";
        public const string NOT_EXISTING_FILE_IDENTIFIER = "nonexistent";
        public const char REVISION_DELIMITER = '\t';
        public const string MODIFIED_FILE_IDENTIFIER = "+++";
        public const string COMMENTS_IDENTIFIER = "'";
        public const string REVISION_PREFIX = "(revision ";
        public const string REVISION_SUFFIX = ")";
       
    }
}
