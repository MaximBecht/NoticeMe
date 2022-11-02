using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;



namespace NoticeMe.Data.ViewModels
{
    public partial class ProfileViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string _userName;
        private string _firstName;
        private string _lastName;
        private string _email;
        private BitmapImage _profileImage;
        private bool _isEditProfilePopupOpen = false;

        private string _editedUserName;
        private string _editedFirstName;
        private string _editedLastName;
        private string _editedEmail;
        private BitmapImage _editedProfileImage;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string UserName
        {
            get
            {
                if (string.IsNullOrEmpty(_userName))
                    return "Username not set";
                return _userName;
            }
            set
            {
                if (_userName != value)
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
                if (_firstName != value)
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
                if (_lastName != value)
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
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                    return "Name name not set";
                return FirstName + " " + LastName;
            }
        }
        public string Email
        {
            get
            {
                if (string.IsNullOrEmpty(_email))
                    return "Email not set";
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public BitmapImage ProfileImage 
        { 
            get => _profileImage;
            set
            {
                if (_profileImage != value)
                {
                    _profileImage = value;
                    OnPropertyChanged("ProfileImage");
                }
            }
        }
        public bool IsEditProfilePopupOpen
        {
            get => _isEditProfilePopupOpen;
            set
            {
                if(_isEditProfilePopupOpen != value)
                {
                    _isEditProfilePopupOpen = value;
                    OnPropertyChanged("IsEditProfilePopupOpen");
                }
            }
        }

        // ToDo: String control REGEX for email formating check and min length for Names and Passwords(+ are numbers and symbols in password? -> dont allow otherwise and show message)
        public string EditedUserName
        {
            get => _editedUserName; 
            set
            {
                if (_editedUserName != value)
                {
                    _editedUserName = value;
                    OnPropertyChanged("EditedUserName");
                }
            }
        }
        public string EditedFirstName
        {
            get => _editedFirstName;
            set
            {
                if (_editedFirstName != value)
                {
                    _editedFirstName = value;
                    OnPropertyChanged("EditedFirstName");
                }
            }
        }
        public string EditedLastName
        {
            get => _editedLastName;
            set
            {
                if (_editedLastName != value)
                {
                    _editedLastName = value;
                    OnPropertyChanged("EditedLastName");
                }
            }
        }
        public string EditedEmail
        {
            get => _editedEmail;
            set
            {
                if (_editedEmail != value)
                {
                    _editedEmail = value;
                    OnPropertyChanged("EditedEmail");
                }
            }
        }
        public BitmapImage EditedProfileImage
        {
            get => _editedProfileImage;
            set
            {
                if (_editedProfileImage != value)
                {
                    _editedProfileImage = value;
                    OnPropertyChanged("EditedProfileImage");
                }
            }
        }

        public void Open_EditProfilePopup_ButtonClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(_userName))
                EditedUserName = UserName;

            EditedFirstName = FirstName;
            EditedLastName = LastName;

            if (!string.IsNullOrEmpty(_email))
                EditedEmail = Email;

            IsEditProfilePopupOpen = true;
        }

        public void Save_EditProfilePopup_ButtonClick(object sender, RoutedEventArgs e)
        {
            UserName = EditedUserName;
            FirstName = EditedFirstName;
            LastName = EditedLastName;
            Email = EditedEmail;
            
            if(EditedProfileImage != null)
                ProfileImage = EditedProfileImage;

            IsEditProfilePopupOpen = false;
        }

        public void Close_EditProfilePopup_ButtonClick(object sender, RoutedEventArgs e)
        {
            IsEditProfilePopupOpen = false;
        }


        //public async void OpenProfilePicturePicker_EditProfilePopup_ButtonClick(object sender, RoutedEventArgs e)
        //{
        //    StorageFile pickedImageFile = await GetImageAsync();
        //    if (pickedImageFile != null)
        //    {
        //        using (IRandomAccessStream fileStream = await pickedImageFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
        //        {
        //            BitmapImage bitmapImage = new BitmapImage();
        //            await bitmapImage.SetSourceAsync(fileStream);

        //            EditedProfileImage = bitmapImage;
        //        }
        //    }
        //}

        //private async Task<StorageFile> GetImageAsync()
        //{
        //    var filePicker = new FileOpenPicker();

        //    //// Get the current window's HWND by passing in the Window object
        //    //var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(_target);

        //    //// Associate the HWND with the file picker
        //    //WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

        //    filePicker.FileTypeFilter.Add(".png");
        //    filePicker.FileTypeFilter.Add(".jpg");
        //    filePicker.FileTypeFilter.Add(".jpeg");
        //    filePicker.FileTypeFilter.Add(".bmp");
        //    filePicker.FileTypeFilter.Add(".webp");

        //    StorageFile file = await filePicker.PickSingleFileAsync();
        //    if (file != null)
        //    {
        //        // Application now has read/write access to the picked file
        //        return file;
        //    }
        //    else
        //    {
        //        // User or something canceled operation;
        //        return null;
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
