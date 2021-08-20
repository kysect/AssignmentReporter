using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.ViewModel.MvvmBase;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.Plugin.ViewModel
{ 
    public class GeneratorSettingsViewModel : BaseViewModel
    {

        private ObservableCollection<string> _generatorTypes = new ObservableCollection<string>(){".pdf", ".docx", ".txt", ".md"} ;
        private string _selectedGeneratorType;
        private string _pathToSave = string.Empty;
        private string _pathToRepository = string.Empty;
        private bool _isMultiGeneration = false;
        private bool _isPdf = true;
        private static CoverPageInfo _coverPageInfo;
        private static FileSearchFilter _filter;
        private static string _introduction = string.Empty;
        private static string _conclusion = string.Empty;

        public GeneratorSettingsViewModel()
        {

        }

        public GeneratorSettingsViewModel(string generatorSettings, FileSearchFilter filter)
        {
            SelectedGeneratorType = generatorSettings;
            Filter = filter;
        }

        public string SelectedGeneratorType
        {
            get => _selectedGeneratorType;
            set
            {
                _selectedGeneratorType = value;
                NotifyPropertyChanged("SelectedGeneratorType");
            }
        }
        public ObservableCollection<string> GeneratorTypes
        {
            get => _generatorTypes;
            set
            {
                _generatorTypes = value;
                NotifyPropertyChanged("GeneratorTypes");
            }
        }
        public string PathToSave
        {
            get => _pathToSave;
            set
            {
                _pathToSave = value;
                NotifyPropertyChanged("PathToSave");
            }
        }
        public string PathToRepository
        {
            get => _pathToRepository;
            set
            {
                _pathToRepository = value;
                NotifyPropertyChanged("PathToRepository");
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

        private RelayCommand _selectPathToRepositoryCommand;
        public RelayCommand SelectPathToRepositoryCommand
        {
            get
            {
                return _selectPathToRepositoryCommand ??
                       (_selectPathToRepositoryCommand = new RelayCommand(obj =>
                       {
                           FolderBrowserDialog dlg = new FolderBrowserDialog();
                           if (dlg.ShowDialog() == DialogResult.OK)
                           {
                               PathToRepository = dlg.SelectedPath;
                           }
                       }));
            }
        }

        private RelayCommand _selectPathToSaveCommand;
        public RelayCommand SelectPathToSaveCommand
        {
            get
            {
                return _selectPathToSaveCommand ??
                       (_selectPathToSaveCommand = new RelayCommand(obj =>
                       {
                           SaveFileDialog saveFileDialog = new SaveFileDialog
                           {
                               Filter = @"pdf files (*.pdf)|*.pdf | doc files (*.docx, .doc) |*.docx *.doc | markdown files (*.md)|*.md | txt files (*.txt)|*.txt",
                               FilterIndex = 4,
                               RestoreDirectory = true
                           };

                           if (saveFileDialog.ShowDialog() == DialogResult.OK)
                           {
                               PathToSave = saveFileDialog.FileName;
                           }
                       }));
            }
        }

        private RelayCommand _generateCommand;
        public RelayCommand GenerateCommand
        {
            get
            {
                return _generateCommand ??
                       (_generateCommand = new RelayCommand(obj =>
                       {
                           FileSystemSourceCodeProvider provider = new FileSystemSourceCodeProvider(PathToRepository, _filter);
                           ReportExtendedInfo info = new ReportExtendedInfo(_introduction, _conclusion, PathToSave);
                           if (!_isMultiGeneration)
                           {
                               if (_coverPageInfo != null)
                               {
                                   GetGenerator(_coverPageInfo).Generate(provider.GetFiles(), info);
                               }
                               else if (_isPdf && _coverPageInfo != null)
                               {
                                   DocumentReportGenerator generator = new DocumentReportGenerator(_coverPageInfo);
                                   generator.Generate(provider.GetFiles(), info);
                                   generator.ConvertToPdf(info);
                               }
                               else
                               {
                                   GetGenerator().Generate(provider.GetFiles(), info);
                               }
                           }
                           else
                           {
                               MultiGenerator multiGenerator =
                                   new MultiGenerator(PathToRepository, PathToSave, GetGenerator(_coverPageInfo), _filter);
                               multiGenerator.Generate();
                           }
                       }));
            }
        }

        private IReportGenerator GetGenerator(CoverPageInfo info = null)
        {
            if (SelectedGeneratorType == ".pdf" || SelectedGeneratorType == ".docx")
            {
                return info == null ? new DocumentReportGenerator() : new DocumentReportGenerator(info);
            }
            if (SelectedGeneratorType == ".txt")
            {
                return new SimpleTextReportGenerator();
            }
            if (SelectedGeneratorType == ".md")
            {
                return new MarkdownReportGenerator();
            }
            throw new Exception("Generator type can't be null");
        }


        public static void TransferFilters(SearchSettings settings)
        {
            _filter = new FileSearchFilter(settings);
        }

        public static void TransferInfo(CoverPageInfo coverPage, string introduction, string conclution)
        {
            _coverPageInfo = coverPage;
            _introduction = introduction;
            _conclusion = conclution;
        }
    }
}
