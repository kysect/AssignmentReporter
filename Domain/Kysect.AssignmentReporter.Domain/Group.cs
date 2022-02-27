using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kysect.AssignmentReporter.Domain;

public class Group
{
    public Group(string name)
    {
        Name = name;
        Students = new List<Student>();
    }

 #pragma warning disable CS8618
    private Group()
    {
    }
 #pragma warning restore CS8618

    [Key]
    public string Name { get; private init; }
    public List<Student> Students { get; private init; }
}