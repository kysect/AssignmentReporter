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
            reportExtendedInfo.Path = CheckExtension(reportExtendedInfo.Path);
            var reportFile = File.Create(reportExtendedInfo.Path);
            reportFile.Close();
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
            File.WriteAllText(reportExtendedInfo.Path, builder.ToString());
            FileInfo info = new FileInfo(reportExtendedInfo.Path);
            return new FileDescriptor(info.Name, File.ReadAllText(info.FullName), info.DirectoryName);
        }
        public string CheckExtension(string path)
        {
            return path.EndsWith(Extension)
                ? path
                : path + Extension;
        }
    }
}