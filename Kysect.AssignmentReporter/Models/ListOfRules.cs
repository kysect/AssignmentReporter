using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public abstract class ListOfRules
    {
        public List<string> FileNames { get; set; } = new List<string>();
        public List<string> FileFormats { get; set; } = new List<string>();
        public List<string> Directories { get; set; } = new List<string>();

        public ListOfRules(List<string> fileNames, List<string> fileFormats, List<string> directories)
        {
            FileNames = fileNames;
            FileFormats = fileFormats;
            Directories = directories;
        }

        public ListOfRules() { }
        public abstract bool FileIsAcceptable(string fileName);
        public abstract bool FormatIsAcceptable(string fileFormat);
        public abstract bool DirectoryIsAcceptable(string directory);
    }
}
