using System;
using System.IO;
using System.Net;
using Kysect.AssignmentReporter.Models;

//using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Report
    {
        private Report() { }

        public Report(
            Subject subject,
            Student student,
            Teacher teacher,
            string workNumber,
            FileEntry file,
            string fileName)
        {
            Id = Guid.Empty;
            Subject = subject;
            Student = student;
            Teacher = teacher;
            File = file;
            WorkNumber = workNumber;
            FileName = fileName;
        }

        public Guid Id { get; private init; }
        public Subject Subject { get; private init; }
        public Student Student { get; private init; }
        public Teacher Teacher { get; private init; }
        public string WorkNumber { get; private init; }
        public string FileName { get; private init; }
        public FileEntry File { get; private init; }
    }
}