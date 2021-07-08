using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.ReportGenerator.MultiGenerator
{
   public class MultiGenerator
   {
        public string RootPath { get; set; }
        public string ReportsPath { get; set; }
        public IReportGenerator Generator { get; set; }

        public FileSearchFilter Filter { get; set; }

        public MultiGenerator(string rootPath, string reportsPath, IReportGenerator generator, FileSearchFilter filter)
        {
            RootPath = rootPath;
            ReportsPath = reportsPath;
            Generator = generator;
            Filter = filter;
        }

        public List<string> GetRepositories()
        {
            return Directory
                .GetDirectories(RootPath)
                .ToList();
        }

        public List<FileDescriptor> Generate()
        {
            return GetRepositories().
                Select(repository
                    => Generator.Generate(new FileSystemSourceCodeProvider(repository, Filter).GetFiles()
                    , new ReportExtendedInfo()
                    {
                        Conclusion = string.Empty,
                        Intro = string.Empty,
                        Path = $@"{ReportsPath}/{new DirectoryInfo(repository).Name}"
                    }))
                .ToList();
        }
   }
}
