using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
   public interface IRulesBuilder
   {
       void AddListOfBlockedFiles(List<string> files);
       void AddListOfBlockedDirectories(List<string> directories);
       void AddListOfBlockedExtensions(List<string> extensions);
       void AddListOfAllowedFiles(List<string> files);
       void AddListOfAllowedDirectories(List<string> directories);
       void AddListOfAllowedExtensions(List<string> extensions);
   }
}
