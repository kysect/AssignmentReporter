using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        string Extension { get; }

        FileDescriptor Generate(FileDescriptor result, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    }
}