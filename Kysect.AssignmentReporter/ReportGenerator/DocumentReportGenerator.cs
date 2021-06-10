using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.AssignmentReporter.Models;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class DocumentReportGenerator : IReportGenerator
    {
        public string Extension => "docx";

        public TitlePageInfo titlePage;
        public DocumentReportGenerator() { }
        public Document AddTitleList(TitlePageInfo info, ReportExtendedInfo reportExtendedInfo)
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
                .Append(info.TeacherName)
                .FontSize(12)
                .Font("Times New Roman");

            titleList.SaveAs(reportExtendedInfo.Path);
            return titleList;
        }

        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            var document = reportExtendedInfo.Path.EndsWith(".docx")
                ? DocX.Create(reportExtendedInfo.Path, DocumentTypes.Document)
                : DocX.Create(reportExtendedInfo.Path, DocumentTypes.Pdf);
            if (titlePage != null)
            {
                var titleList = AddTitleList(titlePage, reportExtendedInfo).Paragraphs;
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

            document.InsertParagraph($"{reportExtendedInfo.Intro}")
                .FontSize(12)
                .Alignment = Alignment.left;

            foreach (var fileContent in files)
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

            document.InsertParagraph($"{reportExtendedInfo.Conclusion}")
                .FontSize(12)
                .Alignment = Alignment.left;

            document.Save(reportExtendedInfo.Path);

            FileInfo documentInfo = new FileInfo(reportExtendedInfo.Path);
            return new FileDescriptor(documentInfo.Name, string.Empty, documentInfo.DirectoryName);
        }
    }
}