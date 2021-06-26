using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class SearchSettings
    {
        public List<string> WhiteFileNames { get; set; } = new List<string>();
        public List<string> WhiteFileFormats { get; set; } = new List<string>();
        public List<Regex> WhiteDirectories { get; set; } = new List<Regex>();
        public List<string> BlackFileNames { get; set; } = new List<string>();
        public List<string> BlackFileFormats { get; set; } = new List<string>();
        public List<Regex> BlackDirectories { get; set; } = new List<Regex>();

        public SearchSettings() { }
        
        public bool FileIsAcceptable(string fileName)
        {
            return WhiteFileNames.Contains(fileName) && !BlackFileNames.Contains(fileName);
        }

        public bool FormatIsAcceptable(string fileFormat)
        {
            return !BlackFileFormats.Contains(fileFormat) && WhiteFileFormats.Contains(fileFormat);
        }

        public bool DirectoryIsAcceptable(string directory)
        {
            return !BlackDirectories
                       .Any(dirName => dirName
                           .IsMatch(directory))
                   &&
                   WhiteDirectories
                       .Any(dirName => dirName
                           .IsMatch(directory));
        }
    }
}
