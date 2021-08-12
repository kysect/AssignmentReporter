﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Models.FileSearchRules;
using Kysect.AssignmentReporter.Plugin.VIewModel;
using Kysect.AssignmentReporter.ReportGenerator;
using Kysect.AssignmentReporter.ReportGenerator.MultiGenerator;
using Kysect.AssignmentReporter.SourceCodeProvider;

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
            SearchSettingsWindow taskWindow = new SearchSettingsWindow();
            taskWindow.Show();
        }

        private void CoverPageSettings_Button_Click(object sender, RoutedEventArgs e)
        {
            CoverPageSettingsWindow taskWindow = new CoverPageSettingsWindow();
            taskWindow.Show();
        }
    }
}