using System;
using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileLists;
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
            BlackList bl = new BlackList(new List<string>() { "starter.cs" }, null, new List<string>() { "Exam_Pattern", "obj", "bin" });
            WhiteList wl = new WhiteList(null, new List<string>() { ".cs" }, null);
            var fileSearchFilter = new FileSearchFilter(bl, wl);

            ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(@"C:\Users\andri\source\repos\ITMO_OOP_2020", fileSearchFilter);
            IReportGenerator reportGenerator = new SimpleTextReportGenerator();
            List<FileDescriptor> fileDescriptors = sourceCodeProvider.GetFiles();
            var result = sourceCodeProvider.GetFiles();
            foreach (var file in result)
            {
                Console.WriteLine(file.Name.Substring(file.Name.IndexOf(".")));
            }
        }
    }
}
