using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record GroupDto(string Name, List<StudentDto> Students);