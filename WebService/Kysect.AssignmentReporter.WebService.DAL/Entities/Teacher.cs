using System.Collections.Generic;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
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

        private Teacher() { }

        public List<SubjectGroup> SubjectGroups { get; private init; }

    }
}