using System;
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
        }

        public static void GenerateSimpleReport()
        {
            var bl = new FileMask(
                new List<string> { "CMakeLists" },
                new List<string> { "md", "DS_Store" },
                new List<string> { "cmake-build-debug", ".idea", ".git", ".github", "bin", "obj" });
            var ex = new FileMask(
                new List<string>(),
                new List<string>() {".cs"},
                new List<string>());
            var fileSearchFilter = new FileSearchFilter(bl, ex);
            ISourceCodeProvider sourceCodeProvider =
                new FileSystemSourceCodeProvider(@"C:\Users\andri\source\repos\trashSol", fileSearchFilter);
            var ReportInfo = new ReportExtendedInfo()
            {
                Intro = "introd",
                Conclusion = "concl",
                Path = "C:\\test\\titleScreen1.docx"
            };
            List<FileContainer> result = sourceCodeProvider.GetFiles();
            DocxReportGenerator gen = new DocxReportGenerator(ReportInfo, result);
            TitlePageInfo info = new TitlePageInfo("tn","m2222", "fn", "dis", "2", @"C:\test\titleScreen.docx");
            gen.GenerateDocx(info);
            foreach (FileContainer file in result)
            {
                Console.WriteLine(file.Directory);
                Console.WriteLine(file.NameWithExtension);
            }
        }
    }
}