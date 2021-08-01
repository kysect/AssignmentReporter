using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Kysect.AssignmentReporter.Models.FileSearchRules;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class SearchSettingsWindow 
    {
        public ISearchSettingsBuilder Builder = new SearchSettingsBuilder();
        public SearchSettings Settings;

        public SearchSettingsWindow()
        {
            InitializeComponent();
        }

        private void get_SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(string_WhiteFileNames.Text))
            {
                Builder.AddAllowedFiles(string_WhiteFileNames.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_WhiteFileFormats.Text))
            {
                Builder.AddAllowedExtensions(string_WhiteFileFormats.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_WhiteDirectories.Text))
            {
                Builder.AddAllowedDirectories(string_WhiteDirectories.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackFileNames.Text))
            {
                Builder.AddBlockedFiles(string_BlackFileNames.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackFileFormats.Text))
            {
                Builder.AddBlockedExtensions(string_BlackFileFormats.Text.Split(',').ToList());
            }

            if (!string.IsNullOrEmpty(string_BlackDirectories.Text))
            {
                Builder.AddBlockedDirectories(string_BlackDirectories.Text.Split(',').ToList());
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
