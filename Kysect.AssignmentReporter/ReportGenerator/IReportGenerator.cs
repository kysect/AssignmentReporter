using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public interface IReportGenerator
    {
        string Extension { get; }
        
        FileContainer Generate(FileDescriptor result, List<FileContainer> files, ReportExtendedInfo reportExtendedInfo);
    }
}