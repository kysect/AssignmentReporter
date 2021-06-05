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
        
    }
}