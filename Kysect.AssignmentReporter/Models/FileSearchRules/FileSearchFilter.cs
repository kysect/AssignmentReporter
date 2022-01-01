using System;
using System.IO;
using Kysect.AssignmentReporter.Common;
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

        public Reasonable<bool> FileIsAcceptable(FileInfo file)
        {
            Reasonable<bool> nameIsAcceptable = NameIsAcceptable(file);
            if (!nameIsAcceptable)
            {
                Log.Verbose($"{nameIsAcceptable.Format()}");
                return nameIsAcceptable;
            }

            Reasonable<bool> formatIsAcceptable = FormatIsAcceptable(file);
            if (!formatIsAcceptable)
            {
                Log.Verbose($"{formatIsAcceptable.Format()}");
                return formatIsAcceptable;
            }

            Reasonable<bool> directoryIsAcceptable = DirectoryIsAcceptable(file);
            if (!directoryIsAcceptable)
            {
                Log.Verbose($"{directoryIsAcceptable.Format()}");
                return directoryIsAcceptable;
            }

            return Reasonable.Create(true);
        }

        public Reasonable<bool> NameIsAcceptable(FileInfo file)
        {
            return SearchSettings.FileIsAcceptable(file.Name);
        }

        public Reasonable<bool> FormatIsAcceptable(FileInfo file)
        {
            return SearchSettings.FormatIsAcceptable(file.Extension);
        }

        public Reasonable<bool> DirectoryIsAcceptable(FileInfo file)
        {
            return SearchSettings.DirectoryIsAcceptable(file.FullName);
        }
    }
}