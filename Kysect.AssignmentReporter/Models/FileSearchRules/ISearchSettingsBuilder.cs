using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
   public interface ISearchSettingsBuilder
   { 
       SearchSettingsBuilder AddBlockedFiles(List<string> files);
       SearchSettingsBuilder AddBlockedDirectories(List<string> directories);
       SearchSettingsBuilder AddBlockedExtensions(List<string> extensions);
       SearchSettingsBuilder AddAllowedFiles(List<string> files);
       SearchSettingsBuilder AddAllowedDirectories(List<string> directories);
       SearchSettingsBuilder AddAllowedExtensions(List<string> extensions);
       SearchSettingsBuilder SetDefaultList();
       SearchSettings Build();
   }
}
