using System.ComponentModel;
 
using System.Runtime.CompilerServices;

namespace smartAgriculture.Base
{

    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public void SetProperty<T>(ref T feild, T value, [CallerMemberName] string prpoName = "")
        {
            feild = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpoName));
        }

    }
}
