using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    }
}