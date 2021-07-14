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

            return WhiteFileNames.Count == 0 || WhiteFileNames.Contains(fileName) & !BlackFileNames.Contains(fileName);
        }

        public bool FormatIsAcceptable(string fileFormat)
        {
            return WhiteFileFormats.Count == 0 || WhiteFileFormats.Contains(fileFormat) & !BlackFileFormats.Contains(fileFormat) ;
        }

        public bool DirectoryIsAcceptable(string directory)
        {
            return !BlackDirectories
                       .Any(dirName => dirName
                           .IsMatch(directory))
                   &
                   WhiteDirectories.Count == 0
                   ||
                   WhiteDirectories
                       .Any(dirName => dirName
                           .IsMatch(directory));
        }
    }
}
