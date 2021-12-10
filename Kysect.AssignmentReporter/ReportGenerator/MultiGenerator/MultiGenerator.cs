using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.ReportGenerator.MultiGenerator
{
   public class MultiGenerator
   {
        public string RootPath { get; }
        public string ReportsPath { get; }
        public IReportGenerator Generator { get;}
        public FileSearchFilter Filter { get;}

        public List<string> GetRepositories()
        {
            return Directory
                .GetDirectories(RootPath)
                .Where(dir => Filter.SearchSettings.DirectoryIsAcceptable(dir))
                .ToList();
        }

        public List<FileDescriptor> Generate()
        {
            List<FileDescriptor> reports = new List<FileDescriptor>();

            foreach (var repository in GetRepositories())
            {
                string intro = string.Empty;
                string conclusion = string.Empty;
                string pathToReport = $@"{ReportsPath}/{new DirectoryInfo(repository).Name}";

                reports.Add(Generator.Generate(new FileSystemSourceCodeProvider(repository, Filter)
                    .GetFiles(), new ReportExtendedInfo(
                    intro,
                    conclusion,
                    pathToReport)));
            }
            return reports;
        }
   }
}
