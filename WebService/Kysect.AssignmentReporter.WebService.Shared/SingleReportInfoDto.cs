using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record SingleReportInfoDto(
    string WorkNumber,
    string Introduction,
    string Conclusion,
    List<string> Folders);