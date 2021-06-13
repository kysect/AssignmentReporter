using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.Polygon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Insert the needed report generator type in generic template
            GenerateReport<MarkdownReportGenerator>();
        }

        public static void GenerateReport<TReportGenerator>() where TReportGenerator : IReportGenerator, new()
        {
            //Assign path to your source folder
            string inputPath = @"";
            //Assign path to your output folder
            string outputPath = @"";

            //Assign file name 
            string fileName = "";

            //Create ignore file mask
            var ignore = new FileMask(
                new List<string> {"CMakeLists"},
                new List<string> {"md", "DS_Store"},
                new List<string> {"cmake-build-debug", ".idea"});
            //Create file mask to except some ignored files 
            var exceptions = new FileMask(
                new List<string>(),
                new List<string>(),
                new List<string>());
            var fileSearchFilter = new FileSearchFilter(ignore, exceptions);

            ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(inputPath, fileSearchFilter);
            var reportGenerator = new TReportGenerator();
            List<FileContainer> files = sourceCodeProvider.GetFiles();

            var result = new FileContainer(fileName, reportGenerator.Extension, outputPath);
            result = reportGenerator.Generate(result, files, null);

            File.WriteAllText(Path.Combine(result.Directory, result.NameWithExtension), result.Content);
        }
    }
}