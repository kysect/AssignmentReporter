﻿using System;
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

namespace Kysect.AssignmentReporter.Plugin
{
    /// <summary>
    /// Логика взаимодействия для SearchSettingsWindow.xaml
    /// </summary>
    public partial class SearchSettingsWindow 
    {
        public string ViewModel { get; set; }

        public SearchSettingsWindow()
        {
            InitializeComponent();
        }

        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }
    }
}
