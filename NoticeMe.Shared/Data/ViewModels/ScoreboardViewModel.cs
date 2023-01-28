using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoticeMe.Data.ViewModels
{
    public partial class ScoreboardViewModel : INotifyPropertyChanged
    {
        public HomeViewModel HomeViewModel { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}