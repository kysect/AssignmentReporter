using System;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record StudentDto(Guid Id, string FullName, MinimalGroupDto Group);