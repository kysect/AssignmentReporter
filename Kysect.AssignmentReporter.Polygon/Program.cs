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
            IReportGenerator generator = new MarkdownReportGenerator();
            //Fill the report configuration, with filter parameters 
            var configuration = new ReportConfiguration("", "", "",
                new FileMask(
                    //Names
                    new List<string> {"CMakeLists"},
                    //Extentions
                    new List<string> {"md", "DS_Store"},
                    //Directories
                    new List<string> {"cmake-build-debug", ".idea"}),
                new FileMask(
                    new List<string>(),
                    new List<string>(),
                    new List<string>()));

            //Pass to the function your generator of choice and configuration
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