using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Words.Saving;
using Kysect.AssignmentReporter.Models;
using Microsoft.Office.Interop.Word;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Document = Xceed.Document.NET.Document;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class DocumentReportGenerator : IReportGenerator
    {
        public CoverPageInfo CoverPage;
        private Document document;
        public DocumentReportGenerator() { }
        public Document WriteInCoverList(CoverPageInfo info, ReportExtendedInfo reportExtendedInfo)
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

        public DocX AddCoverPage(Document coverPage)
        {
            var titleList = coverPage.Paragraphs;
            foreach (var paragraph in titleList)
            {
                document.InsertParagraph(paragraph);
            }
            for (int i = 0; i < 22; i++)
            {
                document.InsertParagraph(String.Empty);
            }
            return document as DocX;
        }

        public void ConvertToPdf()
        {
            throw new NotImplementedException();
        }

        public DocX InsertIntroduction(string introduction)
        {
            document.InsertParagraph("introduction:")
                .FontSize(15)
                .Alignment = Alignment.left;

            document.InsertParagraph($"{introduction}")
                .FontSize(12)
                .Alignment = Alignment.left;
            return document as DocX;
        }

        public DocX InsertConclusion(string conclusion)
        {
            document.InsertParagraph("Conclusion:")
                .FontSize(15)
                .Alignment = Alignment.left;

            document.InsertParagraph($"{conclusion}")
                .FontSize(12)
                .Alignment = Alignment.left;
            return document as DocX;
        }
        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        { 
            document = DocX.Create(reportExtendedInfo.Path, DocumentTypes.Document);
            if (CoverPage != null)
            {
               AddCoverPage(WriteInCoverList(CoverPage, reportExtendedInfo));
            }

            InsertIntroduction(reportExtendedInfo.Intro);
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

            InsertConclusion(reportExtendedInfo.Conclusion);
            document.Save(reportExtendedInfo.Path);

            FileInfo documentInfo = new FileInfo(reportExtendedInfo.Path);
            return new FileDescriptor(documentInfo.Name, string.Empty, documentInfo.DirectoryName);
        }
    }
}