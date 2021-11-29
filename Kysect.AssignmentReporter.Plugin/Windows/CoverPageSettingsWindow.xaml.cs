using Kysect.AssignmentReporter.Plugin.ViewModel;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class CoverPageSettingsWindow
    {
        public CoverPageSettingsWindow()
        {
            InitializeComponent();
            DataContext = new CoverPageSettingsViewModel();
        }

        private void CoverPage_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}