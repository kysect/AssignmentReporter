using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class PdfReportGenerator : IReportGenerator
    {
        public FileContainer Generate(FileDescriptor result,List<FileContainer> files, DirectorySearchFilter directorySearchFilter,
            FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}