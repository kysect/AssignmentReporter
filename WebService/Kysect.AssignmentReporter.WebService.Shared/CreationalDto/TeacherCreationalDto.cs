namespace Kysect.AssignmentReporter.WebService.Shared.CreationalDto;

public class TeacherCreationalDto
{
    public TeacherCreationalDto(string fullName)
    {
        FullName = fullName;
    }

    public string FullName { get; set; }
}