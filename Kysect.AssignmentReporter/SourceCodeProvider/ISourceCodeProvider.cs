using System.Collections.Generic;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.SourceCodeProvider
{
    public interface ISourceCodeProvider
    { 
         List<FileDescriptor> GetFiles();
    }
}