using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models
{
   public class BlockingRules : ListOfRules
   { 
       public BlockingRules(List<string> fileNames, List<string> fileFormats, List<string> directories) 
           : base(fileNames, fileFormats, directories) { }
       public BlockingRules() { }

       public override bool FileIsAcceptable(string fileName)
       {
           return !FileNames.Contains(fileName);
       }
       public override bool FormatIsAcceptable(string fileFormat)
       {
           return FileFormats.Contains(fileFormat);
       }

       public override bool DirectoryIsAcceptable(string directory)
       {
           return !Directories
               .Select(dirName => new Regex(dirName))
               .Any(regDir => regDir.IsMatch(directory));
       }
    }
}
