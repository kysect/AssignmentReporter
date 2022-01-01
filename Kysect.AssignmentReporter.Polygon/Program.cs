using System;
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
        public static string User = string.Empty;
        public static string Token = string.Empty;

        public static void Main()
        {
            //GenerateFromGit();
            //GenerateSimpleReport();
            GenerateOrganization();
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
            var mg = new MultiGenerator(rootPath, reportPath, new MarkdownReportGenerator(), filter);
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
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "FrediKats", "MooseFsClient", filter);
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", "report-result");
            documentReportGenerator.Generate(githubSourceCodeProvider.GetFiles(), info);
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
    }

    public class FakePathFormatter : IPathFormatter
    {
        public string FormatFolderPath(string username, string repository)
        {
            return Path.Combine(@"D:\tmp\github\repos", username, repository);
        }
    }
}