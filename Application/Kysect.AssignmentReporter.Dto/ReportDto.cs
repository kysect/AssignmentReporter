using System;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record ReportDto(
    Guid Id,
    SubjectDto Subject,
    StudentDto Student,
    MinimalTeacherDto Teacher,
    string WorkNumber,
    DateTime Date);