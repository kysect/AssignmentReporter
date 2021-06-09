using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public static class ReportGeneratorExtensions
    {
        public static List<object> GeneratePerFolder(this IReportGenerator reportGenerator, 
            FileDescriptor destination, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            //TODO: here we need to group by folder
            ILookup<string, FileDescriptor> fileGroups = null;
            var result = new List<object>();

            foreach (IGrouping<string, FileDescriptor> fileGroup in fileGroups)
            {
                var report = new FileDescriptor(fileGroup.Key, reportGenerator.Extension, destination.Directory);
                report = reportGenerator.Generate(report, fileGroup.ToList(), reportExtendedInfo);
                result.Add(report);
            }

            return result;
        }
    }
}