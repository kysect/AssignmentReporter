namespace Kysect.AssignmentReporter.Dto;

public record SubjectGroupDto(Guid Id, List<StudentDto> Students, MinimalTeacherDto Teacher, SubjectDto Subject);