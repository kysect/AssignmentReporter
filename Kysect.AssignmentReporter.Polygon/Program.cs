using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.Polygon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GenerateSimpleReport();

            string path = @"/Users/george/Documents/Programming2Sem/Lab2/";

            var sourceProvider = new FileSystemSourceCodeProvider(path);
            var source = new FileContainer("source", "md", path);
            var fileFilter = new FileSearchFilter(new List<string> {"DS_Store", "md"},
                new List<string> {"CMakeLists"});
            var directoryFilter = new DirectorySearchFilter(new List<string> {".idea", "cmake-build-debug"});

            var generator = new MarkdownReportGenerator();

            source = generator.Generate(source, sourceProvider.GetFiles(), directoryFilter, fileFilter,
                new ReportExtendedInfo());

            File.WriteAllText(Path.Combine(source.Directory, source.NameWithExtension), source.Content);
        }

        public static void GenerateSimpleReport()
        {
            //ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(@"C:\Users\andri\source\repos\ITMO_OOP_2020"); IReportGenerator reportGenerator = new SimpleTextReportGenerator();

            //List<FileDescriptor> fileDescriptors = sourceCodeProvider.GetFiles();
            //var directorySearchMask = new DirectorySearchMask();
            //var fileSearchFilter = new FileSearchFilter();
            //var reportExtendedInfo = new ReportExtendedInfo();
            //object result = reportGenerator.Generate(fileDescriptors, directorySearchMask, fileSearchFilter, reportExtendedInfo);

            //ISourceCodeProvider sourceCodeProvider1 = new GithubSourceCodeProvider("TomGnill", "ITMO_SDevTools", @"C:\test", data);
            //var result = sourceCodeProvider1.GetFiles();
            //foreach (var file in result)
            //{
            //    Console.WriteLine($"{file.Name}-{file.Directory}");
            //    Console.WriteLine($"{file.Content}");
            //}
        }
    }
}