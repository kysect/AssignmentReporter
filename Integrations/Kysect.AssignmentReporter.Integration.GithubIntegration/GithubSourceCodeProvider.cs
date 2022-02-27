using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.GithubUtils.Models;
using Kysect.GithubUtils.RepositorySync;

namespace Kysect.AssignmentReporter.Integration.GithubIntegration;

public class GithubSourceCodeProvider : ISourceCodeProvider
{
    private readonly RepositoryFetcher _fetcher;
    private readonly IPathFormatStrategy _formatStrategy;
    private readonly string _ownerName;
    private readonly string _repositoryName;

    public GithubSourceCodeProvider(RepositoryFetcher fetcher, string ownerName, string repositoryName, IPathFormatStrategy formatStrategy)
    {
        ArgumentNullException.ThrowIfNull(fetcher);
        ArgumentNullException.ThrowIfNull(ownerName);
        ArgumentNullException.ThrowIfNull(repositoryName);
        ArgumentNullException.ThrowIfNull(formatStrategy);

        _fetcher = fetcher;
        _ownerName = ownerName;
        _repositoryName = repositoryName;
        _formatStrategy = formatStrategy;
    }

    public IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter)
    {
        string pathToFolder = _fetcher.EnsureRepositoryUpdated(_formatStrategy, new GithubRepository(_ownerName, _repositoryName));
        var fileSystemSourceCodeProvider = new FileSystemSourceCodeProvider(pathToFolder);
        return fileSystemSourceCodeProvider.GetFiles(fileSearchFilter);
    }
}