using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    //public class MarkdownReportGenerator : IReportGenerator
    //{
    //    private static readonly IReadOnlyDictionary<string, string> Extensions = new Dictionary<string, string>
    //    {
    //        {"hpp", "cpp"},
    //        {"h", "cpp"}
    //    };

    //    public string Extension => "md";

    //    public FileDescriptor Generate(FileDescriptor descriptor, List<FileDescriptor> files, ReportExtendedInfo reportExtendedInfo)
    //    {
    //        var builder = new StringBuilder();

    //        foreach (FileDescriptor file in files)
    //        {
    //            string extension = Extensions.ContainsKey(file.Extension) ? Extensions[file.Extension] : file.Extension;

    //            builder.AppendLine("## " + file.NameWithExtension);

    //            builder.Append("```");
    //            builder.AppendLine(extension);

    //            builder.Append(file.Content);

    //            builder.AppendLine("\n```\n");
    //        }

    //        var result = new FileDescriptor(descriptor, builder.ToString());
    //        return result;
    //    }
    //}
}