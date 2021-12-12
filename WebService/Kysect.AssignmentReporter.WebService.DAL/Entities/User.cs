using System;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class User
    {
        public User(Guid id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }

        public User(string fullName)
        {
            Id = Guid.Empty;
            FullName = fullName;
        }

 #pragma warning disable CS8618
        protected User()
        {
        }
 #pragma warning restore CS8618

        public Guid Id { get; private init; }
        public string FullName { get; private init; }
    }
}