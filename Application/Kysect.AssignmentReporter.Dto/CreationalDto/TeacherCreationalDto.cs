namespace Kysect.AssignmentReporter.Dto;

public class TeacherCreationalDto
{
    public TeacherCreationalDto(string fullName)
    {
        FullName = fullName;
    }

    public string FullName { get; set; }
}