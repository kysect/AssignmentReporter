using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record SubjectGroupDto(Guid Id, List<StudentDto> Students, MinimalTeacherDto Teacher, SubjectDto Subject);