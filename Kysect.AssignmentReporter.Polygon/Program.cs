using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.Polygon
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateSimpleReport();
        }

        public static void GenerateSimpleReport()
        {
            ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider();
            IReportGenerator reportGenerator = new SimpleTextReportGenerator();


            List<FileDescriptor> fileDescriptors = sourceCodeProvider.GetFiles();
            var directorySearchMask = new DirectorySearchMask();
            var fileSearchFilter = new FileSearchFilter();
            var reportExtendedInfo = new ReportExtendedInfo();

            object result = reportGenerator.Generate(fileDescriptors, directorySearchMask, fileSearchFilter, reportExtendedInfo);
        }
    }
}
