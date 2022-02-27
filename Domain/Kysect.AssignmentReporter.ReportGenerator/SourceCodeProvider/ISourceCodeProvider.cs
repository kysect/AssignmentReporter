using System.Collections.Generic;

namespace Kysect.AssignmentReporter.ReportGenerator;

public interface ISourceCodeProvider
{
    IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter);
}