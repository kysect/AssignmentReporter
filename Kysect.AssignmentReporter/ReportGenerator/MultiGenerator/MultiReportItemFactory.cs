using System.Collections.Generic;
using System.IO;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.ReportGenerator.MultiGenerator
{
    public class MultiReportItemFactory
    {
        private readonly IReadOnlyCollection<FileSearchFilter> _fileSearchFilters;
        //TODO: custom info per lab
        private readonly ReportExtendedInfo _extendedInfo;

        public MultiReportItemFactory(IReadOnlyCollection<FileSearchFilter> fileSearchFilters, ReportExtendedInfo extendedInfo)
        {
            _fileSearchFilters = fileSearchFilters;
            _extendedInfo = extendedInfo;
        }

        public IReadOnlyCollection<MultiReportItem> Split(ISourceCodeProvider sourceCodeProvider)
        {
            //TODO: find user name
            int i = 1;
            List<MultiReportItem> result = new List<MultiReportItem>();
            foreach (FileSearchFilter fileSearchFilter in _fileSearchFilters)
            {
                var reportExtendedInfo = new ReportExtendedInfo(_extendedInfo.Intro, _extendedInfo.Conclusion, Path.Combine(_extendedInfo.Path, i.ToString()));
                result.Add(new MultiReportItem(sourceCodeProvider, fileSearchFilter, reportExtendedInfo));
                i++;
            }
            return result;
        }
    }
}