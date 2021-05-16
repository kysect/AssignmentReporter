using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private string _localStoragePath;
        private string _repositoryOwner;
        private string _repositoryName;


        public List<FileDescriptor> GetFiles()
        {
            throw new System.NotImplementedException();
        }
    }
}