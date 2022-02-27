namespace Kysect.AssignmentReporter.Dto;

public class SubjectGroupCreationalDto
{
    public SubjectGroupCreationalDto(Guid teacherId, string subjectName)
    {
        TeacherId = teacherId;
        SubjectName = subjectName;
    }

    public Guid TeacherId { get; set; }
    public string SubjectName { get; set; }
}