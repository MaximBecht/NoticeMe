using NoticeMe.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;

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
            UserList = DataManager.GetUserDataAsUserVMCollection();
        }

        public void ChangePageTitle(string title)
        {
            ActivePageTitle = title;
        }

        public void SaveUserData_ButtonClick()
        {
            DataManager.SetUserData(UserList);
        }
    }
}
