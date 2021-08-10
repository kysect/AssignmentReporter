using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings;

namespace Kysect.AssignmentReporter.Plugin.VIewModel
{


    public class GeneratorSettingsViewModel : Notifier
    {
        private bool _isMultiGeneration = false;
        private bool _isPdf = true;
        private string _generatorSettings = string.Empty;
        private CoverPageInfo _coverPageInfo;
        private FileSearchFilter _filter;
        private string _introduction = string.Empty;
        private string _conclusion = string.Empty;

        public GeneratorSettingsViewModel() { }

        public GeneratorSettingsViewModel(string generatorSettings, FileSearchFilter filter)
        {
            GeneratorSettings = generatorSettings;
            Filter = filter;
        }

        public string GeneratorSettings
        {
            get => _generatorSettings;
            set
            {
                _generatorSettings = value;
                NotifyPropertyChanged("GeneratorSettings");
            }
        }
        public string Introduction
        {
            get => _introduction;
            set
            {
                _introduction = value;
                NotifyPropertyChanged("Introduction");
            }
        }

        public string Conclusion
        {
            get => _conclusion;
            set
            {
                _conclusion = value;
                NotifyPropertyChanged("Conclusion");
            }
        }

        public bool MultiGeneration
        {
            get => _isMultiGeneration;
            set
            {
                _isMultiGeneration = value;
                NotifyPropertyChanged("MultiGeneration");
            }
        }

        public bool IsPdf
        {
            get => _isPdf;
            set
            {
                _isPdf = value;
                NotifyPropertyChanged("IsPdf");
            }
        }

        public CoverPageInfo CoverPageInfo
        {
            get => _coverPageInfo;
            set
            {
                _coverPageInfo = value;
                NotifyPropertyChanged("CoverPageInfo");
            }
        }

        public FileSearchFilter Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                NotifyPropertyChanged("Filter");
            }
        }
    }
}
