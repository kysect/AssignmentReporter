namespace Kysect.AssignmentReporter.Dto;

public record ReportDto(
    Guid Id,
    SubjectDto Subject,
    StudentDto Student,
    MinimalTeacherDto Teacher,
    string WorkNumber,
    DateTime Date);