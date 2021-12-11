using System.ComponentModel.DataAnnotations;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Subject
    {
        public Subject(string name)
        {
            Name = name;
        }

        private Subject() { }
        [Key]
        public string Name { get; private init; }
    }
}