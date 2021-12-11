using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public record SubjectDto(string Name)
{
    public static SubjectDto FromSubject(Subject subject)
        => new SubjectDto(subject.Name);
}