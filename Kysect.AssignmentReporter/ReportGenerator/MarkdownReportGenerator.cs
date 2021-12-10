using System.Collections.Generic;
using System.IO;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class MarkdownReportGenerator : IReportGenerator
    {
        public string Extension { get; } = ".md";

        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            MemoryStream stream = GenerateStream(files, reportExtendedInfo);
            FileStream reportFile = File.Create(reportExtendedInfo.Path);
            stream.Position = 0;
            stream.CopyTo(reportFile);
            stream.Dispose();
            FileInfo info = new FileInfo(reportExtendedInfo.Path);
            FileDescriptor descriptor = new FileDescriptor(info.Name, reportFile, info.DirectoryName);
            reportFile.Close();
            return descriptor;
        }

        public MemoryStream GenerateStream(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            reportExtendedInfo.Path = reportExtendedInfo.Path.CheckExtension(Extension);
            MemoryStream stream = new MemoryStream();
            var builder = new StringBuilder();
            foreach (FileDescriptor file in files)
            {
                builder.AppendLine("## " + file.Name);

                builder.Append("```");

                builder.AppendLine(new FileInfo(file.RootDirectory).Extension);

                builder.Append(file.Content);

                builder.AppendLine("\n```\n");
            }
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(bytes, 0, bytes.Length);
            return stream;
        }
    }
}