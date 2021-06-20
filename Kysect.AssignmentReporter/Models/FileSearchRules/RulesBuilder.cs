using System.Collections.Generic;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class RulesBuilder : IRulesBuilder
    {
        private readonly ListOfRules _listOfRules;

        public RulesBuilder()
        {
            _listOfRules = new ListOfRules();
        }

        public RulesBuilder AddBlockedFiles(List<string> files)
        {
            _listOfRules.BlackFileNames = files;
            return this;
        }

        public RulesBuilder AddBlockedDirectories(List<string> directories)
        {
            _listOfRules.BlackDirectories = directories;
            return this;
        }

        public RulesBuilder AddBlockedExtensions(List<string> extensions)
        {
            _listOfRules.BlackFileFormats = extensions;
            return this;
        }

        public RulesBuilder AddAllowedFiles(List<string> files)
        {
            _listOfRules.WhiteFileNames = files;
            return this;
        }

        public RulesBuilder AddAllowedDirectories(List<string> directories)
        {
            _listOfRules.WhiteDirectories = directories;
            return this;
        }

        public RulesBuilder AddAllowedExtensions(List<string> extensions)
        {
            _listOfRules.WhiteFileFormats = extensions;
            return this;
        }

        public RulesBuilder SetDefaultList()
        {
            _listOfRules.BlackDirectories = new List<string> { "obj", "bin", ".git" };
            _listOfRules.WhiteFileFormats = new List<string> {".cs"};
            return this;
        }

        public ListOfRules Get()
        {
            return _listOfRules;
        }
    }   
}
