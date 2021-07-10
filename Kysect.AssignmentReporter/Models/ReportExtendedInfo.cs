namespace Kysect.AssignmentReporter.Models
{ 
    public class ReportExtendedInfo
    { 
        public string Intro { get; set; }
        public string Conclusion { get; set; }
        public string Path { get; set; }

        public ReportExtendedInfo(string path, string intro, string conclusion)
        {
            Path = path;
            Intro = intro;
            Conclusion = conclusion;
        }
    }
}