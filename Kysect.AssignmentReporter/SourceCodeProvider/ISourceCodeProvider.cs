using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public interface ISourceCodeProvider
    {
        IReadOnlyList<FileDescriptor> GetFiles();
    }
}