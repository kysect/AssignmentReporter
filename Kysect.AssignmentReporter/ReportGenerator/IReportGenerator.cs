using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        //TODO: not sure about return type
        FileContainer Generate(FileDescriptor result, List<FileContainer> files,
            DirectorySearchFilter directorySearchFilter,
            FileSearchFilter fileSearchFilter, ReportExtendedInfo reportExtendedInfo);
    }
}