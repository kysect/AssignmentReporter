namespace Kysect.AssignmentReporter.Models
{
    public class ReportExtendedInfo
    {
        public string Intro { get; }
        public string Conclusion { get; }
        public string Path { get; }

        public ReportExtendedInfo(string intro, string conclusion, string path)
        {
            Intro = intro;
            Conclusion = conclusion;
            Path = path;
        }
    }
}