namespace Kysect.AssignmentReporter.Dto;

public class StudentCreationalDto
{
    public StudentCreationalDto(string fullName, string groupName)
    {
        FullName = fullName;
        GroupName = groupName;
    }

    public string FullName { get; init; }
    public string GroupName { get; init; }
}