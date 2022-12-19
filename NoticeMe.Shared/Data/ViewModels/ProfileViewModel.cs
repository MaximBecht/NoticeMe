using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using NoticeMe.Pages;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
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
                    return "Name not set";
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

                    if (EditedProfileImageSourceFile != null)
                    {
                        SaveProfileImage(EditedProfileImageSourceFile);
                    }
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
                    OnPropertyChanged("EditedDisplayName");
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
                    OnPropertyChanged("EditedDisplayName");
                }
            }
        }
        public string EditedDisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(EditedFirstName) || string.IsNullOrEmpty(EditedLastName))
                    return "Name name not set";
                return EditedFirstName + " " + EditedLastName;
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
        public StorageFile EditedProfileImageSourceFile;


        public ProfileViewModel()
        {
            //LoadProfileImage();
        }

        public void OpenEditPageButton(object sender, RoutedEventArgs e)
        {
            PageNavigator.Navigate("Edit Profile", typeof(EditProfilePage));
            InitEditPage();
        }

        public void InitEditPage()
        {
            if (!string.IsNullOrEmpty(_userName))
                EditedUserName = UserName;

            EditedFirstName = FirstName;
            EditedLastName = LastName;

            if (!string.IsNullOrEmpty(_email))
                EditedEmail = Email;

            EditedProfileImageSourceFile = null;
            EditedProfileImage = ProfileImage;
        }

        public void Save_EditProfilePage_ButtonClick(object sender, RoutedEventArgs e)
        {
            UserName = EditedUserName;
            FirstName = EditedFirstName;
            LastName = EditedLastName;
            Email = EditedEmail;

            ProfileImage = EditedProfileImage;

            CloseEditProfilePage();
        }

        public void Close_EditProfilePage_ButtonClick(object sender, RoutedEventArgs e)
        {
            CloseEditProfilePage();
        }

        private void CloseEditProfilePage()
        {
            if (PageNavigator.TryNavigateBack())
            {

            }
        }

        private async void SaveProfileImage(StorageFile sourceFile)
        {
            using (System.IO.Stream fileStream = await sourceFile.OpenStreamForWriteAsync())
            {
                await SavePhoto(fileStream, "profile_image.png");
            }
        }

        private static async Task<string> SavePhoto(Stream photoToSave, string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile photoFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            using (var photoOutputStream = await photoFile.OpenStreamForWriteAsync())
            {
                await photoToSave.CopyToAsync(photoOutputStream);
            }

            return photoFile.Path;
        }

        private async void LoadProfileImage()
        {
            StorageFolder appStorageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile sourceFile = await appStorageFolder.GetFileAsync("profile_image.png");
                if (sourceFile != null)
                {
                    using (IRandomAccessStream fileStream = await sourceFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        await bitmapImage.SetSourceAsync(fileStream);

                        ProfileImage = bitmapImage;
                    }
                }
            }
            catch
            {
                // File not found or something else
            }
        }

        private async Task DeleteProfileImageAsync()
        {
            StorageFolder appStorageFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile sourceFile = await appStorageFolder.GetFileAsync("profile_image.png");
                if (sourceFile != null)
                {
                    await sourceFile.DeleteAsync();

                    EditedProfileImageSourceFile = null;
                    EditedProfileImage = null;
                }
            }
            catch
            {
                // File not found or something else
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
