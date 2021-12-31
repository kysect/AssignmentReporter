using Kysect.GithubUtils;
using Kysect.GithubUtils.RepositoryDiscovering;

namespace Kysect.AssignmentReporter.GithubIntegration;

public class GithubOrganizationReportGenerator
{
    private readonly IPathFormatter _pathFormatter;
    private readonly string _gitUser;
    private readonly string _token;

    public GithubOrganizationReportGenerator(IPathFormatter pathFormatter, string gitUser, string token)
    {
        _pathFormatter = pathFormatter;
        _gitUser = gitUser;
        _token = token;

    }

    public List<GithubOrganizationProcessingItem> Process(string organizationName, bool asParallel = false)
    {
        var gitHubRepositoryDiscoveryService = new GitHubRepositoryDiscoveryService(_token);
        var repositoryFetcher = new RepositoryFetcher(_pathFormatter, _gitUser, _token);

        List<RepositoryRecord> repositoryRecords = GetRepositoryList(gitHubRepositoryDiscoveryService, organizationName).Result;

        if (asParallel)
        {
            var result = repositoryRecords
                .AsParallel()
                .Select(r => SyncRepository(r, repositoryFetcher, organizationName))
                .ToList();

            return result;
        }
        else
        {
            var result = new List<GithubOrganizationProcessingItem>();
            foreach (RepositoryRecord repositoryRecord in repositoryRecords)
            {
                result.Add(SyncRepository(repositoryRecord, repositoryFetcher, organizationName));
            }
            return result;
        }
    }

    private GithubOrganizationProcessingItem SyncRepository(RepositoryRecord repositoryName, RepositoryFetcher repositoryFetcher, string organizationName)
    {
        var path = repositoryFetcher.EnsureRepositoryUpdated(organizationName, repositoryName.Name);
        return new GithubOrganizationProcessingItem(path, organizationName, repositoryName.Name);
    }

    private async Task<List<RepositoryRecord>> GetRepositoryList(GitHubRepositoryDiscoveryService discoveryService, string organizationName)
    {
        var repos = new List<RepositoryRecord>();
        await foreach (RepositoryRecord repositoryRecord in discoveryService.TryDiscover(organizationName))
            repos.Add(repositoryRecord);
        return repos;
    }
}