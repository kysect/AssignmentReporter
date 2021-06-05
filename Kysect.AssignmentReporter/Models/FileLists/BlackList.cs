using System.Collections.Generic;

namespace Kysect.AssignmentReporter.Models.FileLists
{
    public class BlackList
    {
        public List<string> FileNames { get; set; }
        public List<string> FileFormats { get; set; }
        public List<string> Directory { get; set; }

        public BlackList(List<string> fileNames, List<string> fileFormats, List<string> directories)
        {
            FileNames = fileNames;
            FileFormats = fileFormats;
            Directory = directories;
        }

        public bool FileIsNotAcceptable(string fileName)
        {
            bool fileIsAllowed = FileNames?.Contains(fileName) ?? true;
            if (!fileIsAllowed)
            {
                return true;
            }

            return false;
        }
        public bool FormatIsNotAcceptable(string fileFormat)
        {
            bool formatIsAllowed = FileFormats?.Contains(fileFormat) ?? true;
            if (formatIsAllowed)
            {
                return true;
            }

            return false;
        }
    }
}
