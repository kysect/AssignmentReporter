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
            //Create instance of report generator of your choice
            IReportGenerator generator = new MarkdownReportGenerator();
            //Fill the report configuration, with filter parameters 
            var configuration = new ReportConfiguration(
                //Input path
                @"", 
                //Output path
                @"", 
                //Output file name
                "",
                //Ignore file mask
                new FileMask(
                    //Names
                    new List<string> {"CMakeLists"},
                    //Extentions
                    new List<string> {"md", "DS_Store"},
                    //Directories
                    new List<string> {"cmake-build-debug", ".idea"}),
                //Allow file mask
                new FileMask(
                    new List<string>(),
                    new List<string>(),
                    new List<string>()));

            GenerateReport(generator, configuration);
        }

        public static void GenerateReport(IReportGenerator generator, ReportConfiguration configuration)
        {
            var fileSearchFilter = new FileSearchFilter(configuration.Ignore, configuration.Allow);

            ISourceCodeProvider sourceCodeProvider = new FileSystemSourceCodeProvider(configuration.InputPath, fileSearchFilter);
            List<FileContainer> files = sourceCodeProvider.GetFiles();

            var result = new FileContainer(configuration.OutputFileName, generator.Extension, configuration.OutputPath);
            result = generator.Generate(result, files, null);

            File.WriteAllText(Path.Combine(result.Directory, result.NameWithExtension), result.Content);
        }
    }
}