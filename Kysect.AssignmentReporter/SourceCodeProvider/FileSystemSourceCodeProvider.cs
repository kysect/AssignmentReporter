using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private readonly string _rootDirectoryPath;

        private readonly FileSearchFilter _fileSearchFilter;


        public FileSystemSourceCodeProvider(string rootDirectoryPath, FileSearchFilter fileSearchFilter)
        {
            _rootDirectoryPath = rootDirectoryPath;
            _fileSearchFilter = fileSearchFilter;
        }

        public List<FileContainer> GetFiles()
            => Directory
                .EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .Select(i => new FileDescriptor(i))
                .Where(d => _fileSearchFilter.FileIsAcceptable(d))
                .Select(d => new FileContainer(d, File.ReadAllText(d.FileInfo.FullName))).ToList();
    }
}