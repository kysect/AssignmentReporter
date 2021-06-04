using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private readonly string _rootDirectoryPath;

        public FileSystemSourceCodeProvider(string rootDirectoryPath)
        {
            _rootDirectoryPath = rootDirectoryPath;
        }

        public List<FileContainer> GetFiles()
        {
            return Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories)
                .Select(file => new FileInfo(file))
                .Select(info => new FileContainer(info))
                .ToList();
        }
    }
}