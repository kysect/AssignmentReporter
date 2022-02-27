namespace Kysect.AssignmentReporter.ReportGenerator;

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