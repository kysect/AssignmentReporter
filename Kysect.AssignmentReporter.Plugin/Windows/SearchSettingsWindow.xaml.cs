using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    /// <summary>
    /// Логика взаимодействия для SearchSettingsWindow.xaml
    /// </summary>
    public partial class SearchSettingsWindow 
    {
        public ISearchSettingsBuilder Builder = new SearchSettingsBuilder();
        public Kysect.AssignmentReporter.Models.FileSearchRules.SearchSettings Settings;

        public SearchSettingsWindow()
        {
            InitializeComponent();
        }

        private void AddWhiteFiles(List<string> files)
        {
            Builder.AddAllowedFiles(files);
        }
        private void AddWhiteExtensions(List<string> extensions)
        {
            Builder.AddAllowedExtensions(extensions);
        }
        private void AddWhiteDirectories(List<string> directories)
        {
            Builder.AddAllowedDirectories(directories);
        }
        private void AddBlackFiles(List<string> files)
        {
            Builder.AddBlockedFiles(files);
        }
        private void AddBlackExtensions(List<string> extensions)
        {
            Builder.AddBlockedExtensions(extensions);
        }
        private void AddBlackDirectories(List<string> directories)
        {
            Builder.AddBlockedDirectories(directories);
        }
        private void get_SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(string_WhiteFileNames.Text))
            {
                AddWhiteFiles(string_WhiteFileNames.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_WhiteFileFormats.Text))
            {
                AddWhiteExtensions(string_WhiteFileFormats.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_WhiteDirectories.Text))
            {
                AddWhiteDirectories(string_WhiteDirectories.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackFileNames.Text))
            {
                AddBlackFiles(string_BlackFileNames.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackFileFormats.Text))
            {
                AddBlackExtensions(string_BlackFileFormats.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackDirectories.Text))
            {
                AddBlackDirectories(string_BlackDirectories.Text.Split(',').ToList());
            }

            Settings = Builder.Build();
            ToolWindowControl.TransferFilters(Settings);
            this.Close();
        } 
        private void get_SetDefaultSearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            Builder.SetDefaultList();
        }
    }
}
