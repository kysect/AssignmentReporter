using System;
using System.Net;
using Kysect.AssignmentReporter.Models;

//using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Report
    {
        private Report() { }

        public Report(Subject subject, Student student, Teacher teacher, string path)
        {
            Id = Guid.Empty;
            Subject = subject;
            Student = student;
            Teacher = teacher;
            Path = path;
        }

        public Guid Id { get; private init; }
        public Subject Subject { get; private init; }
        public Student Student { get; private init; }
        public Teacher Teacher { get; private init; }
        public string Path { get; private init; }
    }
}