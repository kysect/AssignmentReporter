using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kysect.AssignmentReporter.Models;
using SautinSoft;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Document = Xceed.Document.NET.Document;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class DocumentReportGenerator : IReportGenerator
    {
        public CoverPageInfo CoverPage;
        private Document _document;
        public DocumentReportGenerator() { }
        public Document WriteInCoverList(CoverPageInfo info, ReportExtendedInfo reportExtendedInfo)
        {
            const int parWithWorkNumber = 6;
            const int parWithDiscipline = 7;
            const int parWithFullName = 11;
            const int parWithTeacherName = 12;

            var titleList = DocX.Load(info.PathToTitlePage).Copy();
            var par = titleList.Paragraphs.ToList();

            par[parWithWorkNumber]
                .Append(info.WorkNumber)
                .FontSize(14)
                .Font("Times New Roman");

            par[parWithDiscipline]
                .Append(info.Discipline)
                .FontSize(12)
                .Font("Times New Roman");

            par[parWithFullName]
                .Append(info.GroupNumber + " " + info.FullName)
                .FontSize(12)
                .Font("Times New Roman");

            par[parWithTeacherName]
                .Append(info.TeacherName)
                .FontSize(12)
                .Font("Times New Roman");

            titleList.SaveAs(reportExtendedInfo.Path);
            return titleList;
        }

        public DocX AddCoverPage(Document coverPage)
        {
            const int indentationAmount = 22;
            var titleList = coverPage.Paragraphs;
            foreach (var paragraph in titleList)
            {
                _document.InsertParagraph(paragraph);
            }
            for (int i = 0; i < indentationAmount; i++)
            {
                _document.InsertParagraph(String.Empty);
            }
            return (DocX)_document;
        }

        public void ConvertToPdf(ReportExtendedInfo info)
        {
            new PdfMetamorphosis()
                .DocxToPdfConvertFile(info.Path,
                    info.Path
                        .Replace(".docx", ".pdf"));
        }

        public DocX InsertIntroduction(string introduction)
        {
            _document.InsertParagraph("introduction:")
                .FontSize(15)
                .Alignment = Alignment.left;

            _document.InsertParagraph($"{introduction}")
                .FontSize(12)
                .Alignment = Alignment.left;
            return (DocX)_document;
        }

        public DocX InsertConclusion(string conclusion)
        {
            _document.InsertParagraph("Conclusion:")
                .FontSize(15)
                .Alignment = Alignment.left;

            _document.InsertParagraph($"{conclusion}")
                .FontSize(12)
                .Alignment = Alignment.left;
            return (DocX)_document;
        }
        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        { 
            _document = DocX.Create(reportExtendedInfo.Path, DocumentTypes.Document);
            if (CoverPage != null)
            {
               AddCoverPage(WriteInCoverList(CoverPage, reportExtendedInfo));
            }

            InsertIntroduction(reportExtendedInfo.Intro);
            foreach (var fileContent in files)
            {
                _document.InsertParagraph(fileContent.Name + ":")
                    .Font("Consolas")
                    .FontSize(14)
                    .Alignment = Alignment.center;

                var table = _document.AddTable(1, 1);

                table.Alignment = Alignment.center;
                table.Rows[0].Cells[0].Paragraphs[0]
                    .Append(fileContent.Content)
                    .FontSize(10.5)
                    .Font("Consolas");

                _document.InsertTable(table);
            }

            InsertConclusion(reportExtendedInfo.Conclusion);
            _document.Save();
            FileInfo documentInfo = new FileInfo(reportExtendedInfo.Path);
            return new FileDescriptor(documentInfo.Name, 
                _document.Text,
                documentInfo.DirectoryName);
        }
    }
}