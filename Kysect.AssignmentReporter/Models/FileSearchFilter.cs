using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public ListOfRules BlackList { get; set; }
        public ListOfRules WhiteList { get; set; }

        public FileSearchFilter(ListOfRules blackList, ListOfRules whiteList)
        {
            BlackList = blackList;
            WhiteList = whiteList;
        } 
        public FileSearchFilter(ListOfRules blackList)
        {
            BlackList = blackList;
            WhiteList = null;
        }
        public FileSearchFilter()
        {
            WhiteList = null;
            BlackList = null;
        }
        public bool FileIsAcceptable(FileInfo file)
        {
            return BlackList?.FileIsNotAcceptable(file.Name) ?? (WhiteList?.FileIsAcceptable(file.Name) ?? true);
        }
        public bool FormatIsAcceptable(FileInfo file)
        {
            return WhiteList?.FormatIsAcceptable(file.Extension) ?? (BlackList?.FormatIsNotAcceptable(file.Extension) ?? true);
        }

        public bool DirectoryIsAcceptable(FileInfo file)
        {
            bool blackListAllowed = true;
            bool whiteListAllowed = true;
            if (BlackList?.Directories != null)
            {
                blackListAllowed = !BlackList.Directories
                        .Select(dirName => new Regex(dirName))
                        .Any(regDir => regDir.IsMatch(file.FullName));
            }
            if (WhiteList?.Directories != null)
            {
                whiteListAllowed = WhiteList.Directories
                    .All(dirName => new Regex(dirName)
                        .IsMatch(file.FullName));
            }
            return whiteListAllowed && blackListAllowed;
        }
    }
}