using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared.CreationalDto;

public class SingleReportInfoDto
{
    public SingleReportInfoDto(
        string workNumber,
        string introduction,
        string conclusion,
        List<string> folders)
    {
        WorkNumber = workNumber;
        Introduction = introduction;
        Conclusion = conclusion;
        Folders = folders;
    }

    public string WorkNumber { get; set; }
    public string Introduction { get; set; }
    public string Conclusion { get; set; }
    public List<string> Folders { get; set; }
}