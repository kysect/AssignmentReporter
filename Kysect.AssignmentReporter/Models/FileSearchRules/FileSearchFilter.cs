using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class FileSearchFilter
    {
        public ListOfRules ListOfRules { get; set; }

        public FileSearchFilter(ListOfRules listOfRules)
        {
            ListOfRules = listOfRules;
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
            return ListOfRules.FileIsAcceptable(file.Name);
        }
        public bool FormatIsAcceptable(FileInfo file)
        {
            return ListOfRules.FormatIsAcceptable(file.Extension);
        }

        public bool DirectoryIsAcceptable(FileInfo file)
        {
            return ListOfRules.DirectoryIsAcceptable(file.FullName);
        }
    }
}