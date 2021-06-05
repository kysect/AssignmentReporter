using System.Linq;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Models.FileLists;

namespace Kysect.AssignmentReporter.Models
{
    public class DirectorySearchMask
    {
        public BlackList BlackList { get; set; }
        public WhiteList WhiteList { get; set; }

        public DirectorySearchMask(BlackList blackList, WhiteList whiteList)
        {
            BlackList = blackList;
            WhiteList = whiteList;
        }
        public DirectorySearchMask(BlackList blackList)
        {
            BlackList = blackList;
            WhiteList = null;
        }
        public DirectorySearchMask(WhiteList whiteList)
        {
            WhiteList = whiteList;
            BlackList = null;
        }
        public bool DirectoryIsAcceptable(string filePath)
        {
            bool blackListAllowed = true;
            bool whiteListAllowed = true;
            if (BlackList != null && BlackList.Directory != null)
            {
                if (BlackList.Directory
                    .Select(dirName => new Regex(dirName))
                    .Any(regDir => regDir.IsMatch(filePath)))
                {
                    blackListAllowed = false;
                }
            }
            if (WhiteList != null && WhiteList.Directory != null)
            {
                if (WhiteList.Directory
                    .Any(dirName => !new Regex(dirName).IsMatch(filePath)))
                {
                    whiteListAllowed = false;
                }
            }
            return whiteListAllowed & blackListAllowed;
        }
    }
}