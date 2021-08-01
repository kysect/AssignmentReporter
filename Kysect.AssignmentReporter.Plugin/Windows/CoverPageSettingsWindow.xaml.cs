using System.Windows;
using Kysect.AssignmentReporter.Models;

namespace Kysect.AssignmentReporter.Plugin.Windows
{
    public partial class CoverPageSettingsWindow
    {
        public CoverPageSettingsWindow()
        {
            InitializeComponent();
        }

        private void CoverPage_Button_Click(object sender, RoutedEventArgs e)
        {
            string fullName = string.IsNullOrEmpty(FullName.Text) ? string.Empty : FullName.Text;
            string teacherName = string.IsNullOrEmpty(TeacherName.Text) ? string.Empty : TeacherName.Text;
            string discipline = string.IsNullOrEmpty(Discipline.Text) ? string.Empty : Discipline.Text;
            string workNumber = string.IsNullOrEmpty(WorkNumber.Text) ? string.Empty : WorkNumber.Text;
            string groupName = string.IsNullOrEmpty(GroupNumber.Text) ? string.Empty : GroupNumber.Text;
            string introduction = string.IsNullOrEmpty(Introduction.Text) ? string.Empty : Introduction.Text;
            string conclution = string.IsNullOrEmpty(Conclution.Text) ? string.Empty : Conclution.Text;
            string titleScreen = @"Resources\titleScreen.docx";

            ToolWindowControl.TransferInfo(new CoverPageInfo(
                teacherName,
                groupName, 
                fullName, 
                discipline, 
                workNumber, 
                titleScreen), 
                introduction, 
                conclution);
           Close();
        }
    }
}
