using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
        MemoryStream GenerateStream(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo);
    }
}