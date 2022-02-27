using System;
using System.IO;
using Kysect.CommonLib.Paths;
using Kysect.CommonLib.Reasons;
using Serilog;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class FileSearchFilter
    {
        public FileSearchFilter(SearchSettings searchSettings)
        {
            SearchSettings = searchSettings ?? throw new Exception("Search settings can't be null");
        }

        public SearchSettings SearchSettings { get; set; }

        public Reason<bool> FileIsAcceptable(PartialFilePath filePath)
        {
            Reason<bool> nameIsAcceptable = NameIsAcceptable(filePath.File);
            if (!nameIsAcceptable)
            {
                Log.Verbose($"{nameIsAcceptable.Format()}");
                return nameIsAcceptable;
            }

            Reason<bool> formatIsAcceptable = FormatIsAcceptable(filePath.File);
            if (!formatIsAcceptable)
            {
                Log.Verbose($"{formatIsAcceptable.Format()}");
                return formatIsAcceptable;
            }

            Reason<bool> directoryIsAcceptable = DirectoryIsAcceptable(filePath.ParentDirectoryPath);
            if (!directoryIsAcceptable)
            {
                Log.Verbose($"{directoryIsAcceptable.Format()}");
                return directoryIsAcceptable;
            }

            return Reason.Create(true);
        }

        public Reason<bool> NameIsAcceptable(FileInfo file)
        {
            return SearchSettings.FileIsAcceptable(file.Name);
        }

        public Reason<bool> FormatIsAcceptable(FileInfo file)
        {
            return SearchSettings.FormatIsAcceptable(file.Extension);
        }

        public Reason<bool> DirectoryIsAcceptable(PartialPath directoryPath)
        {
            return SearchSettings.DirectoryIsAcceptable(directoryPath);
        }
    }
}