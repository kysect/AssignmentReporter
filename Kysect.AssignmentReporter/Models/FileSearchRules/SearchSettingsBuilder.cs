﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Kysect.AssignmentReporter.Models.FileSearchRules
{
    public class SearchSettingsBuilder : ISearchSettingsBuilder
    {
        private readonly SearchSettings _searchSettings;

        public SearchSettingsBuilder()
        {
            _searchSettings = new SearchSettings();
        }

        public SearchSettingsBuilder AddBlockedFiles(List<string> files)
        {
            _searchSettings.BlackFileNames = files;
            return this;
        }

        public SearchSettingsBuilder AddBlockedDirectories(List<string> directories)
        {
            _searchSettings.BlackDirectories = directories
                .Select(dir => new Regex(dir))
                .ToList();
            return this;
        }

        public SearchSettingsBuilder AddBlockedExtensions(List<string> extensions)
        {
            _searchSettings.BlackFileFormats = extensions;
            return this;
        }

        public SearchSettingsBuilder AddAllowedFiles(List<string> files)
        {
            _searchSettings.WhiteFileNames = files;
            return this;
        }

        public SearchSettingsBuilder AddAllowedDirectories(List<string> directories)
        {
            _searchSettings.WhiteDirectories = directories
                .Select(dir => new Regex(dir))
                .ToList();
            return this;
        }

        public SearchSettingsBuilder AddAllowedExtensions(List<string> extensions)
        {
            _searchSettings.WhiteFileFormats = extensions;
            return this;
        }

        public SearchSettingsBuilder SetDefaultList()
        {
            AddBlockedDirectories(new List<string>{"obj", "bin", ".git"});
            AddAllowedExtensions(new List<string> { ".cs" });
            return this;
        }

        public SearchSettings Build()
        {
            return _searchSettings;
        }
    }   
}