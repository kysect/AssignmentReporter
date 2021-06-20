using System;
using System.IO;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class FileSearchFilter
    {
        public ListOfRules ListOfRules { get; set; }

        public FileSearchFilter(ListOfRules listOfRules)
        {
            ListOfRules = listOfRules;
            if (listOfRules == null)
            {
                throw new Exception("listOfRules can't be null");
            }
        }
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