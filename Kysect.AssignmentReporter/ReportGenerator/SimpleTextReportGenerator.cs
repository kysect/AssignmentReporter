using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class SimpleTextReportGenerator : IReportGenerator
    {
        public FileDescriptor Generate(List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
        {
            if (!reportExtendedInfo.Path.EndsWith(".txt"))
            {
                reportExtendedInfo.Path += ".txt";
            }
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
    }
}