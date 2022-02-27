using System.Collections.Generic;

namespace Kysect.AssignmentReporter.ReportGenerator;

public class MultiGenerator
{
    private readonly MultiReportItemFactory _itemFactory;
    private readonly IReportGenerator _generator;

    public MultiGenerator(MultiReportItemFactory itemFactory, IReportGenerator generator)
    {
        _itemFactory = itemFactory;
        _generator = generator;
    }

    public IReadOnlyCollection<FileDescriptor> Generate(ISourceCodeProvider sourceCodeProvider, string studentName)
    {
        var result = new List<FileDescriptor>();

        foreach (MultiReportItem multiReportItem in _itemFactory.Split(sourceCodeProvider, studentName))
        {
            FileDescriptor report = _generator.Generate(sourceCodeProvider.GetFiles(multiReportItem.Filter), multiReportItem.ExtendedInfo);
            result.Add(report);
        }

        return result;
    }
}