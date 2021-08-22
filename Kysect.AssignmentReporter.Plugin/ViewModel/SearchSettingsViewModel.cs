using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.ViewModel.MvvmBase;

namespace Kysect.AssignmentReporter.Plugin.ViewModel
{
    public class SearchSettingsViewModel : BaseViewModel
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
        {
            SearchSettings = new RelayCommand(obj =>
            {
                SetSettings();
            });
        }
        public string WhiteFileNames
        {
            get => _whiteFileNames;
            set
            {
                _whiteFileNames = value;
                NotifyPropertyChanged();
            }
        }

        public string WhiteFileFormats
        {
            get => _whiteFileFormats;
            set
            {
                _whiteFileFormats = value;
                NotifyPropertyChanged();
            }
        }

        public string WhiteDirectories
        {
            get => _whiteDirectories;
            set
            {
                _whiteDirectories = value;
                NotifyPropertyChanged();
            }
        }

        public string BlackFileNames
        {
            get => _blackFileNames;
            set
            {
                _blackFileNames = value;
                NotifyPropertyChanged();
            }
        }

        public string BlackFileFormats
        {
            get => _blackFileFormats;
            set
            {
                _blackFileFormats = value;
                NotifyPropertyChanged();
            }
        }

        public string BlackDirectories
        {
            get => _blackDirectories;
            set
            {
                _blackDirectories = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand SearchSettings { get; }

        private void SetSettings()
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
        }
        private RelayCommand _setDefaultSettings;
        public RelayCommand SetDefaultSettings
        {
            get
            {
                return _setDefaultSettings ??
                       (_setDefaultSettings = new RelayCommand(obj =>
                       {
                           Builder.SetDefaultList();
                       }));
            }
        }

    }
}
