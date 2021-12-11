using System;
using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class SubjectGroup
    {
        public SubjectGroup(List<Student> students, Teacher teacher, Subject subject)
        {
            Id = Guid.Empty;
            Teacher = teacher;
            Subject = subject;
            Students = students;
        }

        public SubjectGroup(Teacher teacher, Subject subject)
        {
            Id = Guid.Empty;
            Teacher = teacher;
            Subject = subject;
            Students = new List<Student>();
        }

        private SubjectGroup() { }

        public Guid Id { get; private init; }
        public List<Student> Students { get; private init; }
        public Teacher Teacher { get; private init; }
        public Subject Subject { get; private init; }
    }
}