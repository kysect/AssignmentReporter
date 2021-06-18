using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public ListOfRules BlackList { get; set; } = new BlockingRules(
            new List<string>(), 
            new List<string>(),
            new List<string> { "obj", "bin", ".git"});
        public ListOfRules WhiteList { get; set; } = new AllowRules(
            new List<string>(), 
            new List<string>{ ".cs" }, 
            new List<string>());

        public FileSearchFilter(ListOfRules blackList, ListOfRules whiteList)
        {
            BlackList = blackList;
            WhiteList = whiteList;
        } 
        public FileSearchFilter(ListOfRules rulesList)
        {
            switch (rulesList)
            {
                case AllowRules _:
                    WhiteList = rulesList;
                    break;
                case BlockingRules _:
                    BlackList = rulesList;
                    break;
            }
        }
        public FileSearchFilter()
        { }

        public bool FileIsAcceptable(FileInfo file)
        {
            return NameIsAcceptable(file)
                   && FormatIsAcceptable(file)
                   && DirectoryIsAcceptable(file);
        }
        public bool NameIsAcceptable(FileInfo file)
        {
            return BlackList.FileIsAcceptable(file.Name) && WhiteList.FileIsAcceptable(file.Name);
        }
        public bool FormatIsAcceptable(FileInfo file)
        {
            return WhiteList.FormatIsAcceptable(file.Extension) && BlackList.FormatIsAcceptable(file.Extension);
        }

        public bool DirectoryIsAcceptable(FileInfo file)
        {
            return WhiteList.DirectoryIsAcceptable(file.FullName) && BlackList.DirectoryIsAcceptable(file.FullName);
        }
    }
}