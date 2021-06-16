using System;
using System.Collections.Generic;
using System.IO;
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

        public List<FileDescriptor> GetFiles()
        {
            var files = new List<FileDescriptor>();
            foreach (var file in Directory.EnumerateFiles(_rootDirectoryPath, "*", SearchOption.AllDirectories))
            {
                FileInfo info = new FileInfo(file);
                if  (_fileSearchFilter.DirectoryIsAcceptable(info.DirectoryName)
                     && _fileSearchFilter.FormatIsAcceptable(info.Name) 
                     && _fileSearchFilter.FileIsAcceptable(info.Name))
                {
                    files
                        .Add(new FileDescriptor(info.Name, File.ReadAllText(info.FullName), info.DirectoryName));
                }
            }
           
            return files;
        }

    }
}