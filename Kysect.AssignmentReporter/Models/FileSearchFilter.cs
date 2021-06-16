using System;
using System.Linq;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Models.FileLists;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public BlackList BlackList { get; set; }
        public WhiteList WhiteList { get; set; }

        public FileSearchFilter(BlackList blackList, WhiteList whiteList)
        {
            BlackList = blackList;
            WhiteList = whiteList;
        } 
        public FileSearchFilter(BlackList blackList)
        {
            BlackList = blackList;
            WhiteList = null;
        } 
        public FileSearchFilter (WhiteList whiteList)
        { 
            WhiteList = whiteList;
            BlackList = null;
        }

        public FileSearchFilter()
        {
            WhiteList = null;
            BlackList = null;
        }
        public bool FileIsAcceptable(string fileName)
        {
            bool blackListAllowed = BlackList?.FileIsNotAcceptable(fileName) ?? true;
            bool whiteListAllowed = WhiteList?.FileIsAcceptable(fileName) ?? true;
            return blackListAllowed & whiteListAllowed;
        }
        public bool FormatIsAcceptable(string fileName)
        {
            bool blackListAllowed = BlackList?.FormatIsNotAcceptable(CheckFormat(fileName)) ?? true;
            bool whiteListAllowed = WhiteList?.FormatIsAcceptable(CheckFormat(fileName)) ?? true;
            return whiteListAllowed & blackListAllowed;
        }

        public bool DirectoryIsAcceptable(string filePath)
        {
            bool blackListAllowed = true;
            bool whiteListAllowed = true;
            if (BlackList != null && BlackList.Directories != null)
            {
                if (BlackList.Directories
                    .Select(dirName => new Regex(dirName))
                    .Any(regDir => regDir.IsMatch(filePath)))
                {
                    blackListAllowed = false;
                }
            }
            if (WhiteList != null && WhiteList.Directories != null)
            {
                if (WhiteList.Directories
                    .Any(dirName => !new Regex(dirName).IsMatch(filePath)))
                {
                    whiteListAllowed = false;
                }
            }
            return whiteListAllowed & blackListAllowed;
        }
        public string CheckFormat(string fileName)
        {
            
            return fileName.Contains(".")
                 ? fileName.Substring(fileName.IndexOf(".", StringComparison.Ordinal))
                 : ".dosntHaveExtention";
        }
    }
}