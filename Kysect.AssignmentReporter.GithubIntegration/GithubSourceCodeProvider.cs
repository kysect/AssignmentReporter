using Kysect.AssignmentReporter.Models;
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
            _fetcher = fetcher;
            _ownerName = ownerName;
            _repositoryName = repositoryName;

        }

        public IReadOnlyList<FileDescriptor> GetFiles()
        {
            string pathToFolder = _fetcher.EnsureRepositoryUpdated(_ownerName, _repositoryName);
            List<string> files = Directory.EnumerateFiles(pathToFolder, "*", SearchOption.AllDirectories).ToList();

            var result = new List<FileDescriptor>();
            foreach (var filePath in files)
            {
                var fileInfo = new FileInfo(filePath);
                result.Add(new FileDescriptor(fileInfo.Name, File.ReadAllText(filePath), fileInfo.DirectoryName));
            }

            return result;
        }
    }
}