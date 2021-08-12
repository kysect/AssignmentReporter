﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings;

namespace Kysect.AssignmentReporter.Plugin.VIewModel
{
    public class SearchSettingsViewModel : Notifier
    {
        public ISearchSettingsBuilder Builder = new SearchSettingsBuilder();
        public SearchSettings Settings;

        private string _whiteFileNames;
        private string _whiteFileFormats;
        private string _whiteDirectories;
        private string _blackFileNames;
        private string _blackFileFormats;
        private string _blackDirectories;

        public SearchSettingsViewModel()
        { }
        public string WhiteFileNames
        {
            get => _whiteFileNames;
            set
            {
                _whiteFileNames = value;
                NotifyPropertyChanged("WhiteFileNames");
            }
        }

        public string WhiteFileFormats
        {
            get => _whiteFileFormats;
            set
            {
                _whiteFileFormats = value;
                NotifyPropertyChanged("WhiteFileFormats");
            }
        }

        public string WhiteDirectories
        {
            get => _whiteDirectories;
            set
            {
                _whiteDirectories = value;
                NotifyPropertyChanged("WhiteDirectories");
            }
        }

        public string BlackFileNames
        {
            get => _blackFileNames;
            set
            {
                _blackFileNames = value;
                NotifyPropertyChanged("BlackFileNames");
            }
        }

        public string BlackFileFormats
        {
            get => _blackFileFormats;
            set
            {
                _blackFileFormats = value;
                NotifyPropertyChanged("BlackFileFormats");
            }
        }

        public string BlackDirectories
        {
            get => _blackDirectories;
            set
            {
                _blackDirectories = value;
                NotifyPropertyChanged("BlackDirectories");
            }
        }

        private AssignmentReporterPluginCommand _searchSettings;
        public AssignmentReporterPluginCommand SearchSettings
        {
            get
            {
                return _searchSettings ??
                       (_searchSettings = new AssignmentReporterPluginCommand(obj =>
                       {
                           if (!string.IsNullOrEmpty(WhiteFileNames))
                           {
                               Builder.AddAllowedFiles(WhiteFileNames.Split(',').ToList());
                           }

                           if (!string.IsNullOrEmpty(WhiteFileFormats))
                           {
                               Builder.AddAllowedExtensions(WhiteFileFormats.Split(',').ToList());
                           }

                           if (!string.IsNullOrEmpty(WhiteDirectories))
                           {
                               Builder.AddAllowedDirectories(WhiteDirectories.Split(',').ToList());
                           }

                           if (!string.IsNullOrEmpty(BlackFileNames))
                           {
                               Builder.AddBlockedFiles(BlackFileNames.Split(',').ToList());
                           }

                           if (!string.IsNullOrEmpty(BlackFileFormats))
                           {
                               Builder.AddBlockedExtensions(BlackFileFormats.Split(',').ToList());
                           }

                           if (!string.IsNullOrEmpty(BlackDirectories))
                           {
                               Builder.AddBlockedDirectories(BlackDirectories.Split(',').ToList());
                           }

                           Settings = Builder.Build();
                           GeneratorSettingsViewModel.TransferFilters(Settings);
                       }));
            }
        }

        private AssignmentReporterPluginCommand _setDefaultSettings;
        public AssignmentReporterPluginCommand SetDefaultSettings
        {
            get
            {
                return _setDefaultSettings ??
                       (_setDefaultSettings = new AssignmentReporterPluginCommand(obj =>
                       {
                           Builder.SetDefaultList();
                       }));
            }
        }

    }
}
