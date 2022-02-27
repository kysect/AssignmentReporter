using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record TeacherDto(Guid Id, string FullName, List<MinimalSubjectGroupDto> SubjectGroups);