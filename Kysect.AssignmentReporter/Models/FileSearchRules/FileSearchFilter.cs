using System;
using System.IO;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class FileSearchFilter
    {
        public SearchSettings SearchSettings { get; set; }

        public FileSearchFilter(SearchSettings searchSettings)
        {
            SearchSettings = searchSettings ?? throw new Exception("Search settings can't be null");
        }
        public bool FileIsAcceptable(FileInfo file)
        {
            return NameIsAcceptable(file)
                   && FormatIsAcceptable(file)
                   && DirectoryIsAcceptable(file);
        }
        public bool NameIsAcceptable(FileInfo file)
        {
            return SearchSettings.FileIsAcceptable(file.Name);
        }
        public bool FormatIsAcceptable(FileInfo file)
        {
            return SearchSettings.FormatIsAcceptable(file.Extension);
        }

        public bool DirectoryIsAcceptable(FileInfo file)
        {
            return SearchSettings.DirectoryIsAcceptable(file.FullName);
        }
    }
}