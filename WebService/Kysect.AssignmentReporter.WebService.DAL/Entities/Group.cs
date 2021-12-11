using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Group
    {
        private Group() { }

        public Group(string name)
        {
            Name = name;
            Students = new List<Student>();
        }

        [Key]
        public string Name { get; private init; }
        public List<Student> Students { get; private init; }
    }
}