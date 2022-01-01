using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public interface ISourceCodeProvider
    {
        IReadOnlyList<FileDescriptor> GetFiles(FileSearchFilter fileSearchFilter);
    }
}