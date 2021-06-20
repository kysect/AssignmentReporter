using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class ListOfRules
    {
        public List<string> WhiteFileNames { get; set; } = new List<string>();
        public List<string> WhiteFileFormats { get; set; } = new List<string>();
        public List<string> WhiteDirectories { get; set; } = new List<string>();

        public List<string> BlackFileNames { get; set; } = new List<string>();
        public List<string> BlackFileFormats { get; set; } = new List<string>();
        public List<string> BlackDirectories { get; set; } = new List<string>();
        public ListOfRules() { }
        public bool FileIsAcceptable(string fileName)
        {
            return WhiteFileNames.Contains(fileName) && !BlackFileNames.Contains(fileName);
        }

        public bool FormatIsAcceptable(string fileFormat)
        {
            return !BlackDirectories.Contains(fileFormat) && WhiteFileFormats.Contains(fileFormat);
        }
        public bool DirectoryIsAcceptable(string directory)
        {
            return (!BlackDirectories
                .Select(dirName => new Regex(dirName))
                .Any(regDir => regDir.IsMatch(directory)))
                   &&
                   WhiteDirectories
                       .All(dirName => new Regex(dirName)
                           .IsMatch(directory));
        }
    }
}
