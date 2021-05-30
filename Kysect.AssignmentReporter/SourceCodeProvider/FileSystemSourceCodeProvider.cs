using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private string _rootDirectoryPath;

        public FileSystemSourceCodeProvider(string rootDirectoryPath)
        {
            _rootDirectoryPath = rootDirectoryPath;
        }

        public List<FileDescriptor> GetFiles()
        {
            var files = new List<FileDescriptor>();
            foreach (var file in Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {
                FileInfo info = new FileInfo(file);
                files
                    .Add(new FileDescriptor(info.Name, File.ReadAllText(info.FullName),
                        info.DirectoryName));
            }
            return files;
        }
    }
}