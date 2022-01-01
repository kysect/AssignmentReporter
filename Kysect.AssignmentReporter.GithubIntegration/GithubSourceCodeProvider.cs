using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.SourceCodeProvider;
using Kysect.GithubUtils;

namespace Kysect.AssignmentReporter.GithubIntegration
{
    public class GithubSourceCodeProvider : ISourceCodeProvider
    {
        private readonly RepositoryFetcher _fetcher;
        private readonly string _ownerName;
        private readonly string _repositoryName;

        public GithubSourceCodeProvider(RepositoryFetcher fetcher, string ownerName, string repositoryName)
        {
            ArgumentNullException.ThrowIfNull(fetcher);
            ArgumentNullException.ThrowIfNull(ownerName);
            ArgumentNullException.ThrowIfNull(repositoryName);

            _fetcher = fetcher;
            _ownerName = ownerName;
            _repositoryName = repositoryName;
        }

        public IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter)
        {
            string pathToFolder = _fetcher.EnsureRepositoryUpdated(_ownerName, _repositoryName);
            var fileSystemSourceCodeProvider = new FileSystemSourceCodeProvider(pathToFolder);
            return fileSystemSourceCodeProvider.GetFiles(fileSearchFilter);
        }
    }
}