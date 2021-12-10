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
        private DocX _document;
        public string Extension { get; } = ".docx";

        public DocumentReportGenerator(CoverPageInfo coverPage) : this()
        {
            CoverPage = coverPage;
        }

        public DocumentReportGenerator() { }

        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            reportExtendedInfo.Path = reportExtendedInfo.Path.CheckExtension(Extension);
            FileStream file = File.Create(reportExtendedInfo.Path);
            
            MemoryStream stream = GenerateStream(files, reportExtendedInfo);
            stream.Position = 0;
            stream.CopyTo(file);
            stream.Close();
            
            FileInfo documentInfo = new FileInfo(reportExtendedInfo.Path);
            FileDescriptor descriptor = new FileDescriptor(
                documentInfo.Name, 
                file, 
                documentInfo.DirectoryName);
            file.Close();

            return descriptor;
        }

        public MemoryStream GenerateStream(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            MemoryStream stream = new MemoryStream();
            _document = DocX.Create(stream);

            if (CoverPage != null)
            {
                AddCoverPage(WriteInCoverList(CoverPage));
            }

            InsertIntroduction(reportExtendedInfo.Intro);

            InsertContent(files);

            InsertConclusion(reportExtendedInfo.Conclusion);

            _document.Save();
            return stream;
        }

        private Document WriteInCoverList(CoverPageInfo info)
        {
            const int parWithWorkNumber = 6;
            const int parWithDiscipline = 7;
            const int parWithFullName = 11;
            const int parWithTeacherName = 12;

            var titleList = DocX.Load(info.PathToCoverPage).Copy();
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
            
            return titleList;
        }

        public void ConvertToPdf(ReportExtendedInfo info)
        {
            new PdfMetamorphosis()
                .DocxToPdfConvertFile(info.Path,
                    info.Path
                        .Replace(".docx", ".pdf"));
        }

        private void AddCoverPage(Document coverPage)
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
        }

        private void InsertIntroduction(string introduction)
        {
            _document.InsertParagraph("introduction:")
                .FontSize(15)
                .Alignment = Alignment.left;

            _document.InsertParagraph($"{introduction}")
                .FontSize(12)
                .Alignment = Alignment.left;
        }

        private void InsertConclusion(string conclusion)
        {
            _document.InsertParagraph("Conclusion:")
                .FontSize(15)
                .Alignment = Alignment.left;

            _document.InsertParagraph($"{conclusion}")
                .FontSize(12)
                .Alignment = Alignment.left;
        }

        private void InsertContent(List<FileDescriptor> files)
        {
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
        }
    }
}