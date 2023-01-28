using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NoticeMe.Data.ViewModels
{
    public partial class UserViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _userName;
        private string _firstName;
        private string _lastName;
        private string _email;

        public int Id 
        { 
            get => _id;
            set
            {
                if(_id != value)
                {
                    _id = value;    
                    OnPropertyChanged("Id");
                }
            } 
        }
        public string UserName
        {
            get => _userName;
            set
            {
                if(_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }
        public string FirstName 
        { 
            get => _firstName;
            set
            {
                if(_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                    OnPropertyChanged("DisplayName");
                }
            } 
        }
        public string LastName 
        { 
            get => _lastName;
            set
            {
                if(_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged("LastName");
                    OnPropertyChanged("DisplayName");
                }
            }
        }
        public string DisplayName
        {
            get 
            {
                return FirstName + " " + LastName;
            }
        }
        public string Email 
        { 
            get => _email;
            set
            {
                if(_email != value) 
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}