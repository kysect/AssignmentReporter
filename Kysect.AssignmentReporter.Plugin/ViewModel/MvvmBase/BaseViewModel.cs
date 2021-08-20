using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kysect.AssignmentReporter.Plugin.ViewModel.MvvmBase
{
   public class BaseViewModel : INotifyPropertyChanged
   {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };

       protected void NotifyPropertyChanged(string propertyName)
       {
           PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
       }
   }
}
