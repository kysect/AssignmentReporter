using Kysect.AssignmentReporter.Plugin.ViewModel;

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