using System.IO;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.GithubIntegration;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.OfficeIntegration;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.GithubUtils;

namespace Kysect.AssignmentReporter.Polygon
{
    internal static class Program
    {
        public static void Main()
        {
            GenerateFromGit();
            //GenerateSimpleReport();
        }

        public static void GenerateSimpleReport()
        {
            FileSearchFilter filter = new (new SearchSettings
            {
                WhiteFileFormats = { ".cs" },
                BlackDirectories = { new Regex("bin"), new Regex("obj") },
            });
            var rootPath = @"C:\test\repos";
            var reportPath = @"C:\test\report";
            var mg = new MultiGenerator(rootPath, reportPath, new MarkdownReportGenerator(), filter);
            mg.Generate();
        }

        public static void GenerateFromGit()
        {
            FileSearchFilter filter = new(new SearchSettings
            {
                WhiteFileFormats = { ".cs" },
                BlackDirectories = { new Regex("bin"), new Regex("obj") },
            });

            var user = string.Empty;
            var token = string.Empty;
            var repositoryFetcher = new RepositoryFetcher(new FakePathResolver(), user, token);
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "FrediKats", "MooseFsClient", filter);
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", "report-result");
            documentReportGenerator.Generate(githubSourceCodeProvider.GetFiles(), info);
        }
    }

    public class FakePathResolver : IPathFormatter
    {
        public string FormatFolderPath(string username, string repository)
        {
            return Path.Combine("test", username, repository);
        }
    }
}