using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public static class ReportGeneratorExtensions
    {
        public static List<object> GeneratePerFolder(this IReportGenerator reportGenerator, List<FileDescriptor> files, DirectorySearchMask directorySearchMask, FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            //TODO: here we need to group by folder
            ILookup<string, FileDescriptor> fileGroups = null;
            List<object> result = new List<object>();

            foreach (IGrouping<string, FileDescriptor> fileGroup in fileGroups)
            {
                object report = reportGenerator.Generate(fileGroup.ToList(), directorySearchMask, fileSearchFilter, reportExtendedInfo);
                result.Add(report);
            }

            return result;
        }
    }
}