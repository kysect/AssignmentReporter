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
        public static string User = string.Empty;
        public static string Token = string.Empty;

        public static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            //GenerateFromGit();
            //GenerateSimpleReport();
            //GenerateOrganization();
            GenerateFromGitSplit();
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

        public static void GenerateFromGit()
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
                    new Regex("obj"),
                    new Regex(".git")
                },
            });

            var formatter = new FakePathFormatter();
            var repositoryFetcher = new RepositoryFetcher(formatter, User, Token);
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "FrediKats", "MooseFsClient");
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", "report-result");
            documentReportGenerator.Generate(githubSourceCodeProvider.GetFiles(filter), info);
        }

        public static void GenerateOrganization()
        {
            var filter = new FileSearchFilter(new SearchSettings
            {
                WhiteFileFormats =
                {
                    ".c"
                },
                BlackDirectories =
                {
                    new Regex("bin"),
                    new Regex("obj"),
                    new Regex("\\.git")
                },
            });

            var formatter = new FakePathFormatter();
            var processingItemFactory = new GithubOrganizationProcessingItemFactory(formatter, User, Token);
            var reportGenerator = new DocumentReportGenerator();
            var organizationReportGenerator = new GithubOrganizationReportGenerator(processingItemFactory, reportGenerator, @"D:\tmp\github\reports");
            organizationReportGenerator.Generate(filter, "IS-prog-21-22", string.Empty, string.Empty);
        }

        public static void GenerateFromGitSplit()
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
                    new Regex("obj"),
                    new Regex(".git")
                },
            });

            var formatter = new FakePathFormatter();
            var repositoryFetcher = new RepositoryFetcher(formatter, User, Token);
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "IS-prog-21-22", "username");
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", @"D:\tmp\github\report-result-split");
            var multiGenerator = new MultiGenerator(new MultiReportItemFactory(GenerateFakeFilters(), info), documentReportGenerator);
            multiGenerator.Generate(githubSourceCodeProvider);
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