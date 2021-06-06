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
            var bl = new FileMask(
                new List<string> { "CMakeLists" }, 
                new List<string> {"md", "DS_Store"}, 
                new List<string> { "cmake-build-debug", ".idea" });
            var ex = new FileMask(
                new List<string>(), 
                new List<string>(), 
                new List<string>());
            var fileSearchFilter = new FileSearchFilter(bl, ex);

            ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(@"/Users/george/Documents/Programming2Sem/Lab3", fileSearchFilter);
            var reportGenerator = new SimpleTextReportGenerator();
            List<FileContainer> result = sourceCodeProvider.GetFiles();
            foreach (FileContainer file in result)
            {
                Console.WriteLine(file.NameWithExtension);
            }
        }
    }
}
