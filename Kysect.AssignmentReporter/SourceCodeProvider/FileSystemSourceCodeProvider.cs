using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private readonly FileSearchFilter _fileSearchFilter;
        private readonly string _rootDirectoryPath;

        public FileSystemSourceCodeProvider(string rootDirectoryPath, FileSearchFilter fileSearchFilter)
        {
            _rootDirectoryPath = rootDirectoryPath;
            _fileSearchFilter = fileSearchFilter;
        }

        public List<FileDescriptor> GetFiles()
        {
            var files = new List<FileDescriptor>();
            foreach (var file in Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {
                var info = new FileInfo(file);
                if (_fileSearchFilter.FileIsAcceptable(info))
                {
                    files
                        .Add(new FileDescriptor(
                            info.Name,
                            File.ReadAllText(info.FullName),
                            info.DirectoryName));
                }
            }

            return files;
        }
    }
}