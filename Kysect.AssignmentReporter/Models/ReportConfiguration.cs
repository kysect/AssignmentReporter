namespace Kysect.AssignmentReporter.Models
{
    public class ReportConfiguration
    {
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string OutputFileName { get; set; }
        public FileMask Ignore { get; set; }
        public FileMask Allow { get; set; }

        public ReportConfiguration(string inputPath, string outputPath, string outputFileName, FileMask ignore, FileMask allow)
        {
            InputPath = inputPath;
            OutputPath = outputPath;
            OutputFileName = outputFileName;
            Ignore = ignore;
            Allow = allow;
        }
    }
}