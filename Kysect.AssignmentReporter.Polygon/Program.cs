
using System.Text.RegularExpressions;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;

namespace Kysect.AssignmentReporter.Polygon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GenerateSimpleReport();
        }

        public static void GenerateSimpleReport()
        {
            FileSearchFilter filter = new(new SearchSettings()
            {
                WhiteFileFormats = {".cs"},
                BlackDirectories = {new Regex("bin"), new Regex("obj")}
            });
            MultiGenerator mg = new(@"C:\test\repos", @"C:\test\reports", new MarkdownReportGenerator(), filter);
            mg.Generate();
        }
    }
}