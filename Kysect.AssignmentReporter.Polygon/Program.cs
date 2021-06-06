using System;
using System.Collections.Generic;
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
            var reportGenerator = new MarkdownReportGenerator();
            List<FileContainer> result = sourceCodeProvider.GetFiles();
            foreach (FileContainer file in result)
            {
                Console.WriteLine(file.NameWithExtension);
            }
        }
    }
}