using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class RulesBuilder : IRulesBuilder
    {
        private readonly ListOfRules _listOfRules;

        public RulesBuilder()
        {
            _listOfRules = new ListOfRules();
        }

        public void AddListOfBlockedFiles(List<string> files)
        {
            _listOfRules.BlackFileNames = files;
        }

        public void AddListOfBlockedDirectories(List<string> directories)
        {
            _listOfRules.BlackDirectories = directories;
        }

        public void AddListOfBlockedExtensions(List<string> extensions)
        {
            _listOfRules.BlackFileFormats = extensions;
        }

        public void AddListOfAllowedFiles(List<string> files)
        {
            _listOfRules.WhiteFileNames = files;
        }

        public void AddListOfAllowedDirectories(List<string> directories)
        {
            _listOfRules.WhiteDirectories = directories;
        }

        public void AddListOfAllowedExtensions(List<string> extensions)
        {
            _listOfRules.WhiteFileFormats = extensions;
        }

        public ListOfRules GetDefaultList()
        {
            _listOfRules.BlackDirectories = new List<string> { "obj", "bin", ".git" };
            _listOfRules.WhiteFileFormats = new List<string> {".cs"};
            return _listOfRules;
        }

        public ListOfRules Get()
        {
            return _listOfRules;
        }
    }   
}
