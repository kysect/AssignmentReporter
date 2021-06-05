using System;
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
        public bool FileIsAcceptable(string fileName)
        {
            bool blackListAllowed = BlackList?.FileIsNotAcceptable(fileName) ?? true;
            bool whiteListAllowed = WhiteList?.FileIsAcceptable(fileName) ?? true;
            if (whiteListAllowed & blackListAllowed)
            {
                return true;
            }

            return false;
        }
        public bool FormatIsAcceptable(string fileName)
        {
            bool blackListAllowed = BlackList?.FormatIsNotAcceptable(CheckFormat(fileName)) ?? true;
            bool whiteListAllowed = WhiteList?.FormatIsAcceptable(CheckFormat(fileName)) ?? true;
            if (whiteListAllowed & blackListAllowed)
            {
                return true;
            }

            return false;
        }

        public string CheckFormat(string fileName)
        {
            var file = fileName.Split('.');

            string format = String.Empty;
            for (int i = 1; i < file.Length; i++)
            {
                format += "."+file[i];
            }

            return format;
        }
    }
}