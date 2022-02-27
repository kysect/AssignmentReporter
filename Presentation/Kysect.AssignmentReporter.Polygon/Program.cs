using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Integration.GithubIntegration;
using Kysect.AssignmentReporter.OfficeIntegration;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.GithubUtils;
using Kysect.GithubUtils.OrganizationReplication;
using Kysect.GithubUtils.RepositoryDiscovering;
using Kysect.GithubUtils.RepositorySync;
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
            GenerateOop();
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

        public static void GenerateFromGitSplit()
        {
            var formatter = new UseOwnerAndRepoForFolderNameStrategy(@"D:\tmp\repos");
            var repositoryFetcher = new RepositoryFetcher(new RepositoryFetchOptions(User, Token));
            var githubSourceCodeProvider = new GithubSourceCodeProvider(repositoryFetcher, "IS-prog-21-22", "username", formatter);
            var documentReportGenerator = new DocumentReportGenerator();
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", @"D:\tmp\github\report-result-split");
            var multiReportItemFactory = new MultiReportItemFactory(GenerateFakeFilters(), info);
            var multiGenerator = new MultiGenerator(multiReportItemFactory, documentReportGenerator);
            multiGenerator.Generate(githubSourceCodeProvider, "username");
        }

        public static void GenerateOop()
        {
            var organizationReplicatorPathProvider = new OrganizationReplicatorPathFormatter(@"D:\tmp\repos");
            var discoveryService = new GitHubRepositoryDiscoveryService(Token);
            var repositoryFetcher = new RepositoryFetcher(new RepositoryFetchOptions(User, Token));
            var organizationFetcher = new OrganizationFetcher(discoveryService, repositoryFetcher, organizationReplicatorPathProvider);

            var formatter = new UseOwnerAndRepoForFolderNameStrategy(@"D:\tmp\repos");
            var reportGenerator = new DocumentReportGenerator();
            var organizationReportGenerator = new GithubOrganizationReportGenerator(organizationFetcher, reportGenerator, @"D:\tmp\github\oop-reports");
            var info = new ReportExtendedInfo("Some test intro", "Some conclusion", @"D:\tmp\github\oop-report-result-split");
            var multiReportItemFactory = new MultiReportItemFactory(GenerateOopFilters(), info);
            var multiGenerator = new MultiGenerator(multiReportItemFactory, reportGenerator);
            organizationReportGenerator.Generate("is-oop-y24", multiGenerator);
        }

        public static List<FileSearchFilter> GenerateFakeFilters()
        {
            return new List<FileSearchFilter>()
            {
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "1" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "2" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "3" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "4" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "5" }).Build()),
                new FileSearchFilter(new SearchSettingsBuilder().AddAllowedExtensions(new List<string> { ".c", ".h" }).AddAllowedDirectories(new List<string> { "6" }).Build()),
            };
        }

        public static List<FileSearchFilter> GenerateOopFilters()
        {
            var labList = new List<string>
            {
                "Isu",
                "Shops",
                "IsuExtra",
                "Backups",
                "Banks",
                "BackupsExtra"
            };

            var fileSearchFilters = labList
                .Select(l =>
                    new FileSearchFilter(
                        new SearchSettingsBuilder()
                            .AddAllowedExtensions(new List<string> { ".cs" })
                            .AddAllowedDirectories(new List<string> { l })
                            .AddBlockedDirectories(new List<string> { $"{l}Extra" })
                            .Build()))
                .ToList();

            return fileSearchFilters;
        }
    }
}