using System;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record MinimalSubjectGroupDto(Guid Id, MinimalTeacherDto Teacher, SubjectDto Subject);