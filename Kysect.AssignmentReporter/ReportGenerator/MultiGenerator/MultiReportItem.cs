using System.Collections.Generic;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.ReportGenerator.MultiGenerator
{
    public class MultiReportItem
    {
        public ISourceCodeProvider CodeProvider { get; }
        public FileSearchFilter Filter { get; }
        public ReportExtendedInfo ExtendedInfo { get; }

        public MultiReportItem(ISourceCodeProvider codeProvider, FileSearchFilter filter, ReportExtendedInfo extendedInfo)
        {
            CodeProvider = codeProvider;
            Filter = filter;
            ExtendedInfo = extendedInfo;
        }
    }
}