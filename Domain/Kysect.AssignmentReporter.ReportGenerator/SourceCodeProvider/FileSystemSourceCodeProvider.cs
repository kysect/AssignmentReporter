using System.Collections.Generic;
using System.IO;
using Kysect.CommonLib.Paths;

namespace Kysect.AssignmentReporter.ReportGenerator;

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
            var partialFilePath = new PartialFilePath(_rootDirectoryPath, file);
            if (fileSearchFilter.FileIsAcceptable(partialFilePath))
            {
                var fileContent = File.ReadAllText(partialFilePath.File.FullName);
                var fileDescriptor = new FileDescriptor(partialFilePath.File.Name, fileContent, partialFilePath.ParentDirectoryPath.Value);
                files.Add(fileDescriptor);
            }
        }

        return files;
    }
}