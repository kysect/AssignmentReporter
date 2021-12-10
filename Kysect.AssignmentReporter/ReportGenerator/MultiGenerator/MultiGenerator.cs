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
       public MultiGenerator(string rootPath, string reportsPath, IReportGenerator generator, FileSearchFilter filter)
       {
           RootPath = rootPath;
           ReportsPath = reportsPath;
           Generator = generator;
           Filter = filter;
       }

       public string RootPath { get; }
       public string ReportsPath { get; }
       public IReportGenerator Generator { get; }
       public FileSearchFilter Filter { get; }

       public List<string> GetRepositories()
       {
           return Directory
               .GetDirectories(RootPath)
               .Where(dir => Filter.SearchSettings.DirectoryIsAcceptable(dir))
               .ToList();
       }

       public List<FileDescriptor> Generate()
       {
           return GetRepositories()
               .ConvertAll(repository
                   => Generator.Generate(
                       new FileSystemSourceCodeProvider(repository, Filter).GetFiles(),
                       new ReportExtendedInfo(
                           string.Empty,
                           string.Empty,
                           $"{ReportsPath}/{new DirectoryInfo(repository).Name}")));
       }
    }
}