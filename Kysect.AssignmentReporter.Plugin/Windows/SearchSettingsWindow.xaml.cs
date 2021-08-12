using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.VIewModel;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class SearchSettingsWindow 
    {
        public SearchSettingsWindow()
        {
            InitializeComponent();
            DataContext = new SearchSettingsViewModel();
        }

        private void get_SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
