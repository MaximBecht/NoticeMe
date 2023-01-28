using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace NoticeMe.Data.ViewModels
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private string _activePageTitle = "Home";
        public string ActivePageTitle 
        {
            get => _activePageTitle;
            set 
            {
                if(_activePageTitle != value)
                {
                    _activePageTitle = value;
                    OnPropertyChanged("ActivePageTitle");
                }
            }
        }


        private UserViewModel _selectedUser;

        public ObservableCollection<UserViewModel> UserList { get; set; } = new();
        public UserViewModel SelectedUser 
        { 
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged("SelectedUser");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public MainViewModel()
        {

        }

        public void ChangePageTitle(string title)
        {
            var resourceLoader = ResourceLoader.GetForViewIndependentUse();
            ActivePageTitle = resourceLoader.GetString(title);
        }
        public string GetCurrentPageTitle()
        {
            return ActivePageTitle;
        }
    }
}