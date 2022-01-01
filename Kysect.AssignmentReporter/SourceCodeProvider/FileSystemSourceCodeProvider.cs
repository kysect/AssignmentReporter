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

        public IReadOnlyList<FileDescriptor> GetFiles()
        {
            var files = new List<FileDescriptor>();
            foreach (var file in Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {
                var info = new FileInfo(file);
                if (_fileSearchFilter.FileIsAcceptable(info))
                {
                    var fileContent = File.ReadAllText(info.FullName);
                    var fileDescriptor = new FileDescriptor(info.Name, fileContent, info.DirectoryName);
                    files.Add(fileDescriptor);
                }
            }

            return files;
        }
    }
}