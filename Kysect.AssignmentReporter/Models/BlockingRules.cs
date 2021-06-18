using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models
{
   public class BlockingRules : ListOfRules
   { 
       public BlockingRules(List<string> fileNames, List<string> fileFormats, List<string> directories)
       {
           FileNames = fileNames;
           FileFormats = fileFormats;
           Directories = directories;
       }
    }
}
