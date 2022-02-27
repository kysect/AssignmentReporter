namespace Kysect.AssignmentReporter.ReportGenerator;

public class ReportExtendedInfo
{
    public ReportExtendedInfo(string intro, string conclusion, string path)
    {
        (Path, Intro, Conclusion) = (path, intro, conclusion);
    }

    public string Intro { get; }
    public string Conclusion { get; }
    public string Path { get; set; }
}