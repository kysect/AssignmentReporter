namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Student : User
    {
        public Student(string fullName, Group group)
            : base(fullName)
        {
            Group = group;
        }

 #pragma warning disable CS8618
        private Student()
        {
        }
 #pragma warning restore CS8618

        public Group Group { get; set; }
    }
}