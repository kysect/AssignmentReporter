using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
   public interface IRulesBuilder
   { 
       RulesBuilder AddBlockedFiles(List<string> files);
       RulesBuilder AddBlockedDirectories(List<string> directories);
       RulesBuilder AddBlockedExtensions(List<string> extensions);
       RulesBuilder AddAllowedFiles(List<string> files);
       RulesBuilder AddAllowedDirectories(List<string> directories);
       RulesBuilder AddAllowedExtensions(List<string> extensions);
       RulesBuilder SetDefaultList();
   }
}
