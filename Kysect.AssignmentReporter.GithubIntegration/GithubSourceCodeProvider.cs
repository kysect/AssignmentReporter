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
        private readonly FileSearchFilter _fileSearchFilter;

        public GithubSourceCodeProvider(RepositoryFetcher fetcher, string ownerName, string repositoryName, FileSearchFilter fileSearchFilter)
        {
            ArgumentNullException.ThrowIfNull(fetcher);
            ArgumentNullException.ThrowIfNull(ownerName);
            ArgumentNullException.ThrowIfNull(repositoryName);
            ArgumentNullException.ThrowIfNull(fileSearchFilter);

            _fetcher = fetcher;
            _ownerName = ownerName;
            _repositoryName = repositoryName;
            _fileSearchFilter = fileSearchFilter;
        }

        public IReadOnlyList<FileDescriptor> GetFiles()
        {
            string pathToFolder = _fetcher.EnsureRepositoryUpdated(_ownerName, _repositoryName);
            var fileSystemSourceCodeProvider = new FileSystemSourceCodeProvider(pathToFolder, _fileSearchFilter);
            return fileSystemSourceCodeProvider.GetFiles();
        }
    }
}