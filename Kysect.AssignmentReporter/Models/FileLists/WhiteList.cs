using System.Collections.Generic;

namespace Kysect.AssignmentReporter.Models.FileLists
{
    public class WhiteList
    {
        public List<string> FileNames { get; set; }
        public List<string> FileFormats { get; set; }
        public List<string> Directory { get; set; }

        public WhiteList(List<string> fileNames, List<string> fileFormats, List<string> directories)
        {
            FileNames = fileNames;
            FileFormats = fileFormats;
            Directory = directories;
        }

        public bool FileIsAcceptable(string fileName)
        {
            return FileNames?.Contains(fileName) ?? true;
        }
        public bool FormatIsAcceptable(string fileFormat)
        {
            return FileFormats?.Contains(fileFormat) ?? true;
        }
    }
}
