namespace Kysect.AssignmentReporter.Models
{
    public class CoverPageInfo
    {
        public CoverPageInfo(
            string teacherName,
            string groupNumber,
            string fullName,
            string discipline,
            string workNumber,
            string pathToCoverPage)
        {
            TeacherName = teacherName;
            FullName = fullName;
            Discipline = discipline;
            WorkNumber = workNumber;
            PathToCoverPage = pathToCoverPage;
            GroupNumber = groupNumber;
        }

        public string GroupNumber { get; set; }
        public string WorkNumber { get; set; }
        public string Discipline { get; set; }
        public string FullName { get; set; }
        public string TeacherName { get; set; }
        public string PathToCoverPage { get; set; }
    }
}