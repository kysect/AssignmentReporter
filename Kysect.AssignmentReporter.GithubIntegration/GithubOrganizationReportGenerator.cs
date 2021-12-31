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

    public async Task<List<GithubOrganizationProcessingItem>> Process(string organizationName, bool asParallel = false)
    {
        var gitHubRepositoryDiscoveryService = new GitHubRepositoryDiscoveryService(_token);
        var repositoryFetcher = new RepositoryFetcher(_pathFormatter, _gitUser, _token);

        if (asParallel)
        {
            List<RepositoryRecord> repos = new List<RepositoryRecord>();
            List<GithubOrganizationProcessingItem> result = new List<GithubOrganizationProcessingItem>();

            await foreach (RepositoryRecord repositoryRecord in gitHubRepositoryDiscoveryService.TryDiscover(organizationName))
                repos.Add(repositoryRecord);

            repos
                .AsParallel()
                .ForAll(r =>
                {
                    var path = repositoryFetcher.EnsureRepositoryUpdated(organizationName, r.Name);
                    result.Add(new GithubOrganizationProcessingItem(path, organizationName, r.Name));
                    Console.WriteLine($"Done {r.Name}");
                });
            return result;
        }
        else
        {
            List<GithubOrganizationProcessingItem> result = new List<GithubOrganizationProcessingItem>();
            await foreach (RepositoryRecord repositoryRecord in gitHubRepositoryDiscoveryService.TryDiscover(organizationName))
            {
                var path = repositoryFetcher.EnsureRepositoryUpdated(organizationName, repositoryRecord.Name);
                result.Add(new GithubOrganizationProcessingItem(path, organizationName, repositoryRecord.Name));
            }
            return result;
        }
    }
}