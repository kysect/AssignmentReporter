using System.Collections.Generic;

namespace Kysect.AssignmentReporter.Domain;

public class Teacher : User
{
    public Teacher(string fullName, List<SubjectGroup> subjectGroups)
        : base(fullName)
    {
        SubjectGroups = subjectGroups;
    }

    public Teacher(string fullName)
        : base(fullName)
    {
        SubjectGroups = new List<SubjectGroup>();
    }

 #pragma warning disable CS8618
    private Teacher()
    {
    }
 #pragma warning restore CS8618

    public List<SubjectGroup> SubjectGroups { get; private init; }
}