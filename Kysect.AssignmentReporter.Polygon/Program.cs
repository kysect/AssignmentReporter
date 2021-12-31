using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.GithubIntegration;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.OfficeIntegration;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;
using Kysect.GithubUtils;

namespace Kysect.AssignmentReporter.Polygon
{
    internal static class Program
    {
        public static string User = String.Empty;
        public static string Token = String.Empty;

        public static void Main()
        {
            //GenerateFromGit();
            //GenerateSimpleReport();
            GenerateOrganization();
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
                BlackDirectories = { new Regex("bin"), new Regex("obj"), new Regex(".git") },
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
            FileSearchFilter filter = new(new SearchSettings
            {
                WhiteFileFormats = { ".c" },
                BlackDirectories = { new Regex("bin"), new Regex("obj"), new Regex("\\.git") },
            });

            var formatter = new FakePathFormatter();
            var githubOrganizationReportGenerator = new GithubOrganizationReportGenerator(formatter, User, Token);
            List<GithubOrganizationProcessingItem> processingItems = githubOrganizationReportGenerator.Process("IS-prog-21-22", true);
            foreach (GithubOrganizationProcessingItem processingItem in processingItems)
            {
                var sourceCodeProvider = new FileSystemSourceCodeProvider(processingItem.Path, filter);
                var info = new ReportExtendedInfo(string.Empty, string.Empty, Path.Combine(@"D:\tmp\github\reports", processingItem.RepositoryName));
                var documentReportGenerator = new DocumentReportGenerator();
                documentReportGenerator.Generate(sourceCodeProvider.GetFiles(), info);
                Console.WriteLine($"done {processingItem.RepositoryName}");
            }
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