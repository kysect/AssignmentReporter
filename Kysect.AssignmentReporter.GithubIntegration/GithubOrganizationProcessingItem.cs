namespace Kysect.AssignmentReporter.GithubIntegration;

public class GithubOrganizationProcessingItem
{
    public string Path { get; set; }
    public string OrganizationName { get; set; }
    public string RepositoryName { get; set; }

    public GithubOrganizationProcessingItem(string path, string organizationName, string repositoryName)
    {
        Path = path;
        OrganizationName = organizationName;
        RepositoryName = repositoryName;
    }
}