using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class FileSystemSourceCodeProvider : ISourceCodeProvider
    {
        private string _rootDirectoryPath;

        public List<FileDescriptor> GetFiles()
        {
            //TODO:
            throw new System.NotImplementedException();
        }
    }
}