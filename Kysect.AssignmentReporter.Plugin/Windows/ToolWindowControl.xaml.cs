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

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class ToolWindowControl : System.Windows.Controls.UserControl
    {
        private bool _isMultiGeneration = false;
        private bool _isPdf= true;
        private IReportGenerator _generator = new DocumentReportGenerator();
        private static CoverPageInfo _coverPageInfo;
        private static FileSearchFilter _filter;
        private static string _introduction = string.Empty;
        private static string _conclusion = string.Empty;
        public ToolWindowControl()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
            InitializeComponent();
        }
        private void Generate_Button_Click(object sender, RoutedEventArgs e)
        {
            FileSystemSourceCodeProvider provider = new FileSystemSourceCodeProvider(pathToRepository.Text, _filter);
            ReportExtendedInfo info = new ReportExtendedInfo(_introduction, _conclusion, pathToSave.Text);
            if (!_isMultiGeneration)
            {
                if (_generator is DocumentReportGenerator && _coverPageInfo != null)
                {
                    DocumentReportGenerator generator = new DocumentReportGenerator(_coverPageInfo);
                    generator.Generate(provider.GetFiles(), info);
                }
                else if (_isPdf && _coverPageInfo != null)
                {
                    DocumentReportGenerator generator = new DocumentReportGenerator(_coverPageInfo);
                    generator.Generate(provider.GetFiles(), info);
                    generator.ConvertToPdf(info);
                }
                else
                {
                    _generator.Generate(provider.GetFiles(), info);
                }
            }
            else
            {
                MultiGenerator multiGenerator =
                    new MultiGenerator(pathToRepository.Text, pathToSave.Text, _generator, _filter);
                multiGenerator.Generate();
            }
           
        }
        private void fileFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = (ComboBoxItem)fileFormat.SelectedItem;
            string reportType = cbi.Content.ToString();
            switch (reportType)
            {
                case ".pdf":
                    _generator = new DocumentReportGenerator();
                    _isPdf = true;
                    break;
                case ".docx":
                    _generator = new DocumentReportGenerator();
                    _isPdf = false;
                    break;
                case ".txt":
                    _generator = new SimpleTextReportGenerator();
                    _isPdf = false;
                    break;
                case ".md":
                    _generator = new MarkdownReportGenerator();
                    _isPdf = false;
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
                Filter = @"pdf files (*.pdf)|*.pdf | doc files (*.docx, .doc) |*.docx *.doc | markdown files (*.md)|*.md | txt files (*.txt)|*.txt",
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
            _isMultiGeneration = false;
        }

        private void MultiGenCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _isMultiGeneration = true;
        }

        private void SearchSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            SearchSettingsWindow taskWindow = new SearchSettingsWindow();
            taskWindow.Show();
        }

        private void CoverPageSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            CoverPageSettingsWindow taskWindow = new CoverPageSettingsWindow();
            taskWindow.Show();
        }

        public static void TransferFilters(SearchSettings settings)
        {
            _filter = new FileSearchFilter(settings);
        }

        public static void TransferInfo(CoverPageInfo coverPage, string introduction, string conclution)
        {
            _coverPageInfo = coverPage;
            ToolWindowControl._introduction = introduction;
            ToolWindowControl._conclusion = conclution;
        }
    }
}
