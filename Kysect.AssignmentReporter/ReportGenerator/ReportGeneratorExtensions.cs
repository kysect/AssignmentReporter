using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public static class ReportGeneratorExtensions
    {
        public static List<object> GeneratePerFolder(this IReportGenerator reportGenerator, FileDescriptor destination,
            List<FileContainer> files, DirectorySearchFilter directorySearchFilter,
            FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            //TODO: here we need to group by folder
            ILookup<string, FileDescriptor> fileGroups = null;
            var result = new List<object>();

            foreach (IGrouping<string, FileContainer> fileGroup in fileGroups)
            {
                var report = new FileContainer(fileGroup.Key, destination.Directory);
                report = reportGenerator.Generate(report, fileGroup.ToList(), directorySearchFilter, fileSearchFilter,
                    reportExtendedInfo);
                result.Add(report);
            }

            return result;
        }
    }
}