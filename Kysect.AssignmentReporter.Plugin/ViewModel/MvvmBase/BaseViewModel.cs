using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Kysect.AssignmentReporter.Plugin.ViewModel.MvvmBase
{
   public class BaseViewModel : INotifyPropertyChanged
   {
       public event PropertyChangedEventHandler PropertyChanged = delegate { };

       protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
       }
   }
}
