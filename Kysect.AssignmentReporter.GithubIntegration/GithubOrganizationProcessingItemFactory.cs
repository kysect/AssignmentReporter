using Kysect.GithubUtils;
using Kysect.GithubUtils.RepositoryDiscovering;
using Serilog;

namespace Kysect.AssignmentReporter.GithubIntegration;

public class GithubOrganizationProcessingItemFactory
{
    private readonly IPathFormatter _pathFormatter;
    private readonly string _gitUser;
    private readonly string _token;

    public GithubOrganizationProcessingItemFactory(IPathFormatter pathFormatter, string gitUser, string token)
    {
        ArgumentNullException.ThrowIfNull(pathFormatter);
        ArgumentNullException.ThrowIfNull(gitUser);
        ArgumentNullException.ThrowIfNull(token);

        _pathFormatter = pathFormatter;
        _gitUser = gitUser;
        _token = token;
    }

    public List<GithubOrganizationProcessingItem> Process(string organizationName, bool asParallel = false)
    {
        var gitHubRepositoryDiscoveryService = new GitHubRepositoryDiscoveryService(_token);
        var repositoryFetcher = new RepositoryFetcher(_pathFormatter, _gitUser, _token);

        Log.Information($"Start discovering repositories from {organizationName}");
        List<RepositoryRecord> repositoryRecords = GetRepositoryList(gitHubRepositoryDiscoveryService, organizationName).Result;
        Log.Information($"Discovered {repositoryRecords.Count} repositories");

        if (asParallel)
        {
            Log.Information("Start parallel processing");

            var result = repositoryRecords
                .AsParallel()
                .Select(r => SyncRepository(r, repositoryFetcher, organizationName))
                .ToList();

            return result;
        }
        else
        {
            Log.Information("Start single thread processing");

            var result = repositoryRecords
                .Select(r => SyncRepository(r, repositoryFetcher, organizationName))
                .ToList();

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