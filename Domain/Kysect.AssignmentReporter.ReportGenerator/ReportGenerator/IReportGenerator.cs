using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        FileDescriptor Generate(IReadOnlyList<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
        MemoryStream GenerateStream(IReadOnlyList<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    }
}