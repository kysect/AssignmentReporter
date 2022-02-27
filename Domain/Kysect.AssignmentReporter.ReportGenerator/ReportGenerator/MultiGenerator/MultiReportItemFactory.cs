using System.Collections.Generic;
using System.IO;

namespace Kysect.AssignmentReporter.ReportGenerator;

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

    public IReadOnlyCollection<MultiReportItem> Split(ISourceCodeProvider sourceCodeProvider, string studentName)
    {
        //TODO: specify lab type
        int i = 1;
        var result = new List<MultiReportItem>();
        foreach (FileSearchFilter fileSearchFilter in _fileSearchFilters)
        {
            var folderPath = Path.Combine(_extendedInfo.Path, studentName);
            Directory.CreateDirectory(folderPath);
            var newPath = Path.Combine(folderPath, i.ToString());
            var reportExtendedInfo = new ReportExtendedInfo(_extendedInfo.Intro, _extendedInfo.Conclusion, newPath);
            result.Add(new MultiReportItem(sourceCodeProvider, fileSearchFilter, reportExtendedInfo));
            i++;
        }
        return result;
    }
}