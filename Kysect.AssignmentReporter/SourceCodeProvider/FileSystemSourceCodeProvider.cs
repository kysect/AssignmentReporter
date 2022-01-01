using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private readonly string _rootDirectoryPath;

        public FileSystemSourceCodeProvider(string rootDirectoryPath)
        {
            _rootDirectoryPath = rootDirectoryPath;
        }

        public IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter)
        {
            var files = new List<FileDescriptor>();
            foreach (var file in Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {
                var info = new FileInfo(file);
                if (fileSearchFilter.FileIsAcceptable(info))
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