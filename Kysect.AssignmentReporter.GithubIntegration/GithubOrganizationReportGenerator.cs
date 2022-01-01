using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.GithubIntegration;

public class GithubOrganizationReportGenerator
{
    private readonly GithubOrganizationProcessingItemFactory _processingItemFactory;
    private readonly IReportGenerator _reportGenerator;
    private readonly string _rootDirectory;

    public GithubOrganizationReportGenerator(GithubOrganizationProcessingItemFactory processingItemFactory, IReportGenerator reportGenerator, string rootDirectory)
    {
        _processingItemFactory = processingItemFactory;
        _reportGenerator = reportGenerator;
        _rootDirectory = rootDirectory;
    }

    public void Generate(FileSearchFilter filter, string organizationName, string intro, string conclusion)
    {
        List<GithubOrganizationProcessingItem> processingItems = _processingItemFactory.Process(organizationName, true);
        foreach (GithubOrganizationProcessingItem processingItem in processingItems)
        {
            var sourceCodeProvider = new FileSystemSourceCodeProvider(processingItem.Path, filter);
            var info = new ReportExtendedInfo(intro, conclusion, Path.Combine(_rootDirectory, processingItem.RepositoryName));
            _reportGenerator.Generate(sourceCodeProvider.GetFiles(), info);
        }
    }
}