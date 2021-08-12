using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.AssignmentReporter.Models;
using Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings;
using Microsoft.Internal.VisualStudio.PlatformUI;

namespace Kysect.AssignmentReporter.Plugin.VIewModel
{
   public class CoverPageSettingsViewModel : Notifier
   {
       private string _fullName;
       private string _teacherName;
       private string _discipline;
       private string _workNumber;
       private string _groupNumber;
       private string _introduction;
       private string _conclusion;
       private string _titleScreen = @"Resources\titleScreen.docx";

       public CoverPageSettingsViewModel()
       {

       }
       public string FullName
       {
           get => _fullName;
           set
           {
               _fullName = value;
               NotifyPropertyChanged("FullName");
           }
       }

       public string TeacherName
       {
           get => _teacherName;
           set
           {
               _teacherName = value;
               NotifyPropertyChanged("TeacherName");
           }
       }

       public string Discipline
        {
           get => _discipline;
           set
           {
               _discipline = value;
               NotifyPropertyChanged("Discipline");
           }
       }

       public string WorkNumber
        {
           get => _workNumber;
           set
           {
               _workNumber = value;
               NotifyPropertyChanged("WorkNumber");
           }
       }

       public string GroupNumber
        {
           get => _groupNumber;
           set
           {
               _groupNumber = value;
               NotifyPropertyChanged("GroupNumber");
           }
       }

       public string Introduction
        {
           get => _introduction;
           set
           {
               _introduction = value;
               NotifyPropertyChanged("Introduction");
           }
       }

       public string Conclusion
        {
           get => _conclusion;
           set
           {
               _conclusion = value;
               NotifyPropertyChanged("Conclusion");
           }
       }

       private AssignmentReporterPluginCommand _coverPageSettings;
       public AssignmentReporterPluginCommand CoverPageSettings
       {
           get
           {
               return _coverPageSettings ??
                      (_coverPageSettings = new AssignmentReporterPluginCommand(obj =>
                      {
                          GeneratorSettingsViewModel.TransferInfo(new CoverPageInfo(
                                  string.IsNullOrEmpty(TeacherName) ? string.Empty : TeacherName,
                                  string.IsNullOrEmpty(GroupNumber) ? string.Empty : GroupNumber,
                                  string.IsNullOrEmpty(FullName) ? string.Empty : FullName,
                                  string.IsNullOrEmpty(Discipline) ? string.Empty : Discipline,
                                  string.IsNullOrEmpty(WorkNumber) ? string.Empty : WorkNumber,
                                  _titleScreen),
                              string.IsNullOrEmpty(Introduction) ? string.Empty : Introduction,
                              string.IsNullOrEmpty(Conclusion) ? string.Empty : Conclusion);
                      }));
           }
       }
    }
}
