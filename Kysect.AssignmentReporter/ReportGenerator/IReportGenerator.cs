using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        string Extension { get; }

        FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    }
}