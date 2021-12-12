using System;

namespace Kysect.AssignmentReporter.WebService.DAL.Entities
{
    public class Report
    {
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

 #pragma warning disable CS8618
        private Report()
        {
        }
 #pragma warning restore CS8618

        public Guid Id { get; private init; }

        public DateTime Date { get; private set; } = DateTime.Now;
        public Subject Subject { get; private init; }
        public Student Student { get; private init; }
        public Teacher Teacher { get; private init; }
        public string WorkNumber { get; private init; }
        public string FileName { get; private init; }
        public FileEntry File { get; private init; }
    }
}