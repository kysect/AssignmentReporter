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

        public FileContainer Generate(FileDescriptor descriptor, List<FileContainer> files,
            DirectorySearchFilter directorySearchFilter, FileSearchFilter fileSearchFilter,
            ReportExtendedInfo reportExtendedInfo)
        {
            var builder = new StringBuilder();

            foreach (FileContainer file in files.Where(file =>
                fileSearchFilter.IsAcceptable(file) && directorySearchFilter.IsAcceptable(file)))
            {
                string extension = file.Extension.Remove(0, 1);
                extension = Extensions.ContainsKey(extension) ? Extensions[extension] : extension;

                builder.Append("\n## ");
                builder.Append(file.NameWithExtension);
                builder.AppendLine();

                builder.Append("```");
                builder.Append(extension);
                builder.AppendLine();

                builder.Append(file.Content);

                builder.Append("\n```\n\n");
            }

            var result = new FileContainer(descriptor, builder.ToString());
            return result;
        }
    }
}