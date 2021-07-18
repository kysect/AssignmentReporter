using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

namespace Kysect.AssignmentReporter.Plugin
{
    /// <summary>
    /// Логика взаимодействия для ToolWindowControl.xaml
    /// </summary>
    public partial class ToolWindowControl : System.Windows.Controls.UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindowControl"/> class.
        /// </summary>
        private bool isMultiGeneration = false;
        private IReportGenerator generator = new DocumentReportGenerator();
        private CoverPageInfo coverPageInfo;
        private FileSearchFilter filter;
        public ToolWindowControl()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            InitializeComponent();
        }
        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            FileSystemSourceCodeProvider provider = new FileSystemSourceCodeProvider(pathToRepository.Text, filter);
            ReportExtendedInfo info = new ReportExtendedInfo(string.Empty, string.Empty, pathToSave.Text);
            if (isMultiGeneration)
            { 
                generator.Generate(provider.GetFiles(), info);
            }
            else
            {
                MultiGenerator multiGenerator =
                    new MultiGenerator(pathToRepository.Text, pathToSave.Text, generator, filter);
                multiGenerator.Generate();
            }
           
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void fileFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)fileFormat.SelectedItem;
            string reportType = cbi.Content.ToString();
            switch (reportType)
            {
                case ".pdf":
                    generator = new DocumentReportGenerator();
                    break;
                case ".docx":
                    generator = new DocumentReportGenerator();
                    break;
                case ".txt":
                    generator = new SimpleTextReportGenerator();
                    break;
                case ".md":
                    generator = new MarkdownReportGenerator();
                    break;
            }
        }

        private void Select_pathToRepository_Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pathToRepository.Text = dlg.SelectedPath;
            }
        }

        private void Select_pathToSave_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "pdf files (*.pdf)|*.pdf | doc files (*.docx, .doc) |*.docx *.doc | markdown files (*.md)|*.md | txt files (*.txt)|*.txt",
                FilterIndex = 4,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pathToSave.Text = saveFileDialog.FileName;
            }
        }

        private void MultiGenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isMultiGeneration = false;
        }

        private void MultiGenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isMultiGeneration = true;
        }

        private void SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            SearchSettingsWindow taskWindow = new SearchSettingsWindow();
        }
    }
}
