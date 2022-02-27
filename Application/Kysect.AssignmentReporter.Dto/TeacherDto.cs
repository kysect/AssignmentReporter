namespace Kysect.AssignmentReporter.Dto;

public record TeacherDto(Guid Id, string FullName, List<MinimalSubjectGroupDto> SubjectGroups);