﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kysect.AssignmentReporter.Plugin.VIewModel.BillingGenerationsSettings
{
   public class Notifier : INotifyPropertyChanged
   {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };

       protected void NotifyPropertyChanged(string propertyName)
       {
           PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
       }
   }
}
