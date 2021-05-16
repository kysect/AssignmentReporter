using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class SimpleTextReportGenerator : IReportGenerator
    {
        public object Generate(List<FileDescriptor> files, DirectorySearchMask directorySearchMask, FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}