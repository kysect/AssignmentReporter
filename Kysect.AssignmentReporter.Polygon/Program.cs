using System;
using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileLists;
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
            //BlackList bl = new BlackList(new List<string>() { "starter.cs", "config", "description" }, null, new List<string>() { "Exam_Pattern", "obj", "bin" });
            //WhiteList wl = new WhiteList(null, new List<string>() { ".cs" }, null);
            //var fileSearchFilter = new FileSearchFilter(bl, wl);

            //ISourceCodeProvider sourceCodeProvider =
            //    new FileSystemSourceCodeProvider(@"C:\Users\andri\source\repos\trashSol", fileSearchFilter);
            //var ReportInfo = new ReportExtendedInfo()
            //{
            //    Intro = "introd",
            //    Conclusion = "concl",
            //    Path = "C:\\test\\titleScreen1.docx"
            //};
            //List<FileDescriptor> result = sourceCodeProvider.GetFiles();
            //DocxReportGenerator gen = new DocxReportGenerator(ReportInfo, result);
            //TitlePageInfo info = new TitlePageInfo("tn", "m2222", "fn", "dis", "2", @"C:\test\titleScreen.docx");
            //gen.GenerateDocx(info);
        }
    }
}