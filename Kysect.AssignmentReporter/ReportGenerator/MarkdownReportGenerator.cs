using System.Collections.Generic;
using System.IO;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class MarkdownReportGenerator : IReportGenerator
    {
        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            var reportFile = File.Create(reportExtendedInfo.Path);
            reportFile.Close();
            var builder = new StringBuilder();
            foreach (FileDescriptor file in files)
            {
                builder.AppendLine("## " + file.Name);

                builder.Append("```");

                builder.Append(file.Content);

                builder.AppendLine("\n```\n");
            }
            File.WriteAllText(reportExtendedInfo.Path, builder.ToString());
            FileInfo info = new FileInfo(reportExtendedInfo.Path);
            return new FileDescriptor(info.Name, File.ReadAllText(info.FullName), info.DirectoryName);
        }
    }
}