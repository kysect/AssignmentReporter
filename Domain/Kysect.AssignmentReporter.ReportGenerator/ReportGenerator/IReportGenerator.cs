using System.Collections.Generic;
using System.IO;

namespace Kysect.AssignmentReporter.ReportGenerator;

public interface IReportGenerator
{
    FileDescriptor Generate(IReadOnlyList<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    MemoryStream GenerateStream(IReadOnlyList<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
}