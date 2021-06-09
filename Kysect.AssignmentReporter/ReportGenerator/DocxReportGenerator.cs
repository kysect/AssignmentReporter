using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.AssignmentReporter.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class DocxReportGenerator : IReportGenerator
    {
        public string Extension => "docx";

        public ReportExtendedInfo ReportInfo { get; set; }
        public List<FileDescriptor> Files { get; set; }

        public DocxReportGenerator(ReportExtendedInfo reportInfo, List<FileDescriptor> files)
        {
            Files = files;
            ReportInfo = reportInfo;
        }
        public string GenerateDocx(TitlePageInfo titlePage)
        {
            var document = DocX.Create(ReportInfo.Path, DocumentTypes.Document);
            if (titlePage != null)
            {
                var titleList = AddTitleList(titlePage).Paragraphs;
                foreach (var paragraph in titleList)
                {
                    document.InsertParagraph(paragraph);
                }

                for (int i = 0; i < 22; i++)
                {
                    document.InsertParagraph(String.Empty);
                }

            }

            document.InsertParagraph("introduction:")
                .FontSize(15)
                .Alignment = Alignment.left;

            document.InsertParagraph($"{ReportInfo.Intro}")
                .FontSize(12)
                .Alignment = Alignment.left;

            foreach (var fileContent in Files)
            {
                document.InsertParagraph(fileContent.Name + ":")
                    .Font("Consolas")
                    .FontSize(14)
                    .Alignment = Alignment.center;

                var table = document.AddTable(1, 1);

                table.Alignment = Alignment.center;
                table.Rows[0].Cells[0].Paragraphs[0]
                    .Append(fileContent.Content)
                    .FontSize(10.5)
                    .Font("Consolas");

                document.InsertTable(table);

            }
            document.InsertParagraph("Conclusion:")
                .FontSize(15)
                .Alignment = Alignment.left;

            document.InsertParagraph($"{ReportInfo.Conclusion}")
                .FontSize(12)
                .Alignment = Alignment.left;
           
            document.Save(ReportInfo.Path);

            return ReportInfo.Path;
        }

        public Document AddTitleList(TitlePageInfo info)
        {
            var titleList = DocX.Load(info.PathToTitlePage).Copy();
            var par = titleList.Paragraphs.ToList();

            par[6]
                .Append(info.WorkNumber)
                .FontSize(14)
                .Font("Times New Roman");

            par[7]
                .Append(info.Discipline)
                .FontSize(12)
                .Font("Times New Roman");

            par[11]
                .Append(info.GroupNumber + " " + info.FullName)
                .FontSize(12)
                .Font("Times New Roman");

            par[12]
                .Append(info.TicherName)
                .FontSize(12)
                .Font("Times New Roman");

            titleList.SaveAs(ReportInfo.Path);
            return titleList;
        }

        public FileDescriptor Generate(FileDescriptor result, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            throw new NotImplementedException();
        }
    }
}