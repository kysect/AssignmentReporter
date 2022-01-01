using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.GithubIntegration;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.OfficeIntegration;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.GithubUtils;
using Serilog;

namespace Kysect.AssignmentReporter.Polygon
{
    internal static class Program
    {
        public static string User => Credentials.User;
        public static string Token => Credentials.Token;

        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            //GenerateFromGit();
            //GenerateSimpleReport();
            GenerateOrganizationSplit();
            //GenerateFromGitSplit();
        }

        public static void GenerateSimpleReport()
        {
            var filter = new FileSearchFilter(new SearchSettings
            {
                WhiteFileFormats =
                {
                    ".cs"
                },
                BlackDirectories =
                {
                    new Regex("bin"),
                    new Regex("obj")
                },
            });
            var rootPath = @"C:\test\repos";
            var reportPath = @"C:\test\report";
            var mg = new LegacyMultiGenerator(rootPath, reportPath, new MarkdownReportGenerator(), filter);
            mg.Generate();
        }

        public static void GenerateOrganizationSplit()
        {
            var formatter = new FakePathFormatter();
            var processingItemFactory = new GithubOrganizationProcessingItemFactory(formatter, User, Token);
            var reportGenerator = new DocumentReportGenerator();
            var organizationReportGenerator = new GithubOrganizationReportGenerator(processingItemFactory, reportGenerator, @"D:\tmp\github\reports");
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", @"D:\tmp\github\report-result-split");
            var multiReportItemFactory = new MultiReportItemFactory(GenerateFakeFilters(), info);
            var multiGenerator = new MultiGenerator(multiReportItemFactory, reportGenerator);
            organizationReportGenerator.Generate("IS-prog-21-22", multiGenerator);
        }

        public static void GenerateFromGitSplit()
        {
            var formatter = new FakePathFormatter();
            var repositoryFetcher = new RepositoryFetcher(formatter, User, Token);
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "IS-prog-21-22", "username");
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", @"D:\tmp\github\report-result-split");
            var multiReportItemFactory = new MultiReportItemFactory(GenerateFakeFilters(), info);
            var multiGenerator = new MultiGenerator(multiReportItemFactory, documentReportGenerator);
            multiGenerator.Generate(githubSourceCodeProvider, "username");
        }

        public static List<FileSearchFilter> GenerateFakeFilters()
        {
            return new List<FileSearchFilter>()
            {
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub1" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub2" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub3" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub4" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub5" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedDirectories(new List<string> { "lub6" }).Build()),
            };
        }
    }

    public class FakePathFormatter : IPathFormatter
    {
        public string FormatFolderPath(string username, string repository)
        {
            return Path.Combine(@"D:\tmp\github\repos", username, repository);
        }
    }
}