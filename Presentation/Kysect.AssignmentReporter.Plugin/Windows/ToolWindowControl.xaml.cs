using System.Globalization;
using System.Threading;
using System.Windows;
using Kysect.AssignmentReporter.Plugin.ViewModel;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class ToolWindowControl : System.Windows.Controls.UserControl
    {
        public ToolWindowControl()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            InitializeComponent();
            DataContext = new GeneratorSettingsViewModel();
        }

        private void SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new SearchSettingsWindow();
            taskWindow.Show();
        }

        private void CoverPageSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new CoverPageSettingsWindow();
            taskWindow.Show();
        }
    }
}