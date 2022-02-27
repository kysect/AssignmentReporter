using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.GithubUtils.RepositorySync;
using Serilog;

namespace Kysect.AssignmentReporter.Integration.GithubIntegration;

public class GithubOrganizationReportGenerator
{
    private readonly OrganizationFetcher _processingItemFactory;
    private readonly IReportGenerator _reportGenerator;
    private readonly string _rootDirectory;

    public GithubOrganizationReportGenerator(OrganizationFetcher processingItemFactory, IReportGenerator reportGenerator, string rootDirectory)
    {
        ArgumentNullException.ThrowIfNull(processingItemFactory);
        ArgumentNullException.ThrowIfNull(reportGenerator);
        ArgumentNullException.ThrowIfNull(rootDirectory);

        _processingItemFactory = processingItemFactory;
        _reportGenerator = reportGenerator;
        _rootDirectory = rootDirectory;
    }

    public void Generate(FileSearchFilter filter, string organizationName, string intro, string conclusion)
    {
        IReadOnlyCollection<GithubOrganizationRepository> processingItems = _processingItemFactory.Fetch(organizationName);
        foreach (GithubOrganizationRepository processingItem in processingItems)
        {
            Log.Information($"Generating reports for {processingItem.OrganizationName}/{processingItem.RepositoryName}");
            var sourceCodeProvider = new FileSystemSourceCodeProvider(processingItem.Path);
            var info = new ReportExtendedInfo(intro, conclusion, Path.Combine(_rootDirectory, processingItem.RepositoryName));
            _reportGenerator.Generate(sourceCodeProvider.GetFiles(filter), info);
        }
    }

    public void Generate(string organizationName, MultiGenerator multiGenerator)
    {
        IReadOnlyCollection<GithubOrganizationRepository> processingItems = _processingItemFactory.Fetch(organizationName);
        foreach (GithubOrganizationRepository processingItem in processingItems)
        {
            Log.Information($"Generating report for {processingItem.RepositoryName}");
            var sourceCodeProvider = new FileSystemSourceCodeProvider(processingItem.Path);
            multiGenerator.Generate(sourceCodeProvider, processingItem.RepositoryName);
        }
    }
}