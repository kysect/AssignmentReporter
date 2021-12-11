using System;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class SubjectGroup
    {
        public SubjectGroup(Group group, Teacher teacher, Subject subject)
        {
            Id = Guid.Empty;
            Group = group;
            Teacher = teacher;
            Subject = subject;
        }

        private SubjectGroup() { }

        public Guid Id { get; private init; }
        public Group Group { get; private init; }
        public Teacher Teacher { get; private init; }
        public Subject Subject { get; private init; }
    }
}