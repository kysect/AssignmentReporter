using Kysect.AssignmentReporter.WebService.DAL.Entities;

namespace Kysect.AssignmentReporter.WebService.Shared;

public class SubjectDto
{
    public SubjectDto(string name)
    {
        this.Name = name;
    }

    public string Name { get; set; }

    public static SubjectDto FromSubject(Subject subject)
        => new SubjectDto(subject.Name);
}