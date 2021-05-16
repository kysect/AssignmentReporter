using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        //TODO: not sure about return type
        object Generate(List<FileDescriptor> files, DirectorySearchMask directorySearchMask, FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo);
    }
}