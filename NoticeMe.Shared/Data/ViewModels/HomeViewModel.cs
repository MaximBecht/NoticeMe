using NoticeMe.Data.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoticeMe.Data.ViewModels
{
    public partial class HomeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IoTDevice> IoTDevices { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}