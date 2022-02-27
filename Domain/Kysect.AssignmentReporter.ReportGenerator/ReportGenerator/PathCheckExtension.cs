namespace Kysect.AssignmentReporter.ReportGenerator
{
    public static class PathCheckExtension
    {
        public static string CheckExtension(this string path, string extension)
        {
            return path.EndsWith(extension)
                ? path
                : path + extension;
        }
    }
}