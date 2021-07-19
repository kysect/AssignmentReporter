using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.Plugin
{
    /// <summary>
    /// Логика взаимодействия для SearchSettingsWindow.xaml
    /// </summary>
    public partial class SearchSettingsWindow 
    {
        public string ViewModel { get; set; }

        public ISearchSettingsBuilder builder = new SearchSettingsBuilder();
        public Kysect.AssignmentReporter.Models.FileSearchRules.SearchSettings settings;

        public SearchSettingsWindow()
        {
            InitializeComponent();
        }

        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }

        private void AddWhiteFiles(List<string> files)
        {
            builder.AddAllowedFiles(files);
        }
        private void AddWhiteExtensions(List<string> extensions)
        {
            builder.AddAllowedExtensions(extensions);
        }
        private void AddWhiteDirectories(List<string> directories)
        {
            builder.AddAllowedDirectories(directories);
        }
        private void AddBlackFiles(List<string> files)
        {
            builder.AddBlockedFiles(files);
        }
        private void AddBlackExtensions(List<string> extensions)
        {
            builder.AddBlockedExtensions(extensions);
        }
        private void AddBlackDirectories(List<string> directories)
        {
            builder.AddBlockedDirectories(directories);
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

            settings = builder.Build();
            ToolWindowControl.TransferFilters(settings);
            this.Close();
        } 
        private void get_SetDefaultSearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            builder.SetDefaultList();
        }
    }
}
