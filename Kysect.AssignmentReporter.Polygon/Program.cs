using System;
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
            //ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(@"C:\Users\andri\source\repos\ITMO_OOP_2020"); IReportGenerator reportGenerator = new SimpleTextReportGenerator();

            // List<FileDescriptor> fileDescriptors = sourceCodeProvider.GetFiles();
            //var directorySearchMask = new DirectorySearchMask();
            //var fileSearchFilter = new FileSearchFilter();
            //var reportExtendedInfo = new ReportExtendedInfo();
            //object result = reportGenerator.Generate(fileDescriptors, directorySearchMask, fileSearchFilter, reportExtendedInfo);
           // ISourceCodeProvider sourceCodeProvider1 = new GithubSourceCodeProvider("TomGnill", "NYSS", data);
           // var result = sourceCodeProvider1.GetFiles();
        }
    }
}
