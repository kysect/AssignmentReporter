namespace Kysect.AssignmentReporter.Models
{
    public class ReportExtendedInfo
    {
        public string Intro { get; }
        public string Conclusion { get; }
        public string Path { get; set; }

        public ReportExtendedInfo(string intro, string conclusion, string path)
        {
            (Path, Intro, Conclusion) = (path, intro, conclusion);
        }
    }
}