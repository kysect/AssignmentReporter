using System.Collections.Generic;
using System.IO;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class SimpleTextReportGenerator : IReportGenerator
    {
        public string Extension { get; } = ".txt";

        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            reportExtendedInfo.Path = reportExtendedInfo.Path.CheckExtension(Extension);
            var reportFile = File.Create(reportExtendedInfo.Path);
            MemoryStream stream = GenerateStream(files, reportExtendedInfo);
            stream.Position = 0;
            stream.CopyTo(reportFile);
            stream.Close();
            FileInfo info = new FileInfo(reportExtendedInfo.Path);
            FileDescriptor descriptor = new FileDescriptor(
                info.Name, 
                reportFile,
                info.DirectoryName);
            reportFile.Close();
            return descriptor;
        }

        public MemoryStream GenerateStream(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(reportExtendedInfo.Intro))
            {
                builder.AppendLine("Introduction:" + reportExtendedInfo.Intro);

                builder.AppendLine("\n\n");
            }
            foreach (FileDescriptor file in files)
            {
                builder.AppendLine("File name:" + file.Name);

                builder.AppendLine("\n");

                builder.AppendLine(new FileInfo(file.RootDirectory).Extension);

                builder.Append(file.Content);

                builder.AppendLine("\n\n");
            }
            if (!string.IsNullOrEmpty(reportExtendedInfo.Conclusion))
            {
                builder.AppendLine("Introduction:" + reportExtendedInfo.Conclusion);
            }
            MemoryStream stream = new MemoryStream();
            byte[] bytes = Encoding.UTF8.GetBytes(builder.ToString());
            stream.Write(bytes, 0, bytes.Length);
            return stream;
        }
    }
}