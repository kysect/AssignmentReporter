namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Student : User
    {
        public Student(string fullName, Group group)
            : base(fullName)
        {
            Group = group;
        }

        private Student() { }

        public Group Group { get; private init; }
    }
}