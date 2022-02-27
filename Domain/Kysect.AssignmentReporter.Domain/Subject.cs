namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Subject
    {
        public Subject(string name)
        {
            Name = name;
        }

 #pragma warning disable CS8618
        private Subject()
        {
        }
 #pragma warning restore CS8618

        public string Name { get; private init; }
    }
}