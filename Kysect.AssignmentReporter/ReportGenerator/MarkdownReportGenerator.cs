using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.ReportGenerator
{
    public class MarkdownReportGenerator : IReportGenerator
    {
        private static readonly IReadOnlyDictionary<string, string> Extensions = new Dictionary<string, string>
        {
            {"hpp", "cpp"},
            {"h", "cpp"}
        };

        public string Extension => "md";

        public FileContainer Generate(FileDescriptor descriptor, List<FileContainer> files,
            DirectorySearchFilter directorySearchFilter, FileSearchFilter fileSearchFilter,
            ReportExtendedInfo reportExtendedInfo)
        {
            var builder = new StringBuilder();

            foreach (FileContainer file in files.Where(file =>
                fileSearchFilter.IsAcceptable(file) && directorySearchFilter.IsAcceptable(file)))
            {
                string extension = Extensions.ContainsKey(file.Extension) ? Extensions[file.Extension] : file.Extension;

                builder.AppendLine("## " + file.NameWithExtension);

                builder.Append("```");
                builder.AppendLine(extension);

                builder.Append(file.Content);

                builder.AppendLine("\n```\n");
            }

            var result = new FileContainer(descriptor, builder.ToString());
            return result;
        }
    }
}