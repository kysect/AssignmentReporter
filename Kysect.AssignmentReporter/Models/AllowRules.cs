using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
    public class AllowRules : ListOfRules
    { 
        public AllowRules(List<string> fileNames, List<string> fileFormats, List<string> directories)
            : base(fileNames, fileFormats, directories) { }
        public AllowRules() { }

        public override bool FileIsAcceptable(string fileName)
        {
            return FileNames.Contains(fileName);
        }

        public override bool FormatIsAcceptable(string fileFormat)
        {
            return FileFormats.Contains(fileFormat);
        }

        public override bool DirectoryIsAcceptable(string directory)
        {
            return Directories
                .All(dirName => new Regex(dirName)
                    .IsMatch(directory));
        }
    }
}
