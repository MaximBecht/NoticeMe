using NoticeMe.Data.DataModels;
using NoticeMe.Data.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace NoticeMe.Data
{
    public static class DataManager
    {
        /// <summary>
        /// Timespan between automatic saves of all application data (Settings[App + Device], User Account, Styling, ...).<br/><br/>
        /// Unit: [min]
        /// </summary>
        private static int _autoSaveTimeSpan = 5; // ToDo: Add this parameter to something like a SettingsXML Data class for saving in seperate storage file.

        public static UserDataXML UserDataXML { get; set; }

        #region FileHandling
        public static async void InitAutoSaver()
        {
            var timer = new PeriodicTimer(TimeSpan.FromMinutes(_autoSaveTimeSpan));

            while (await timer.WaitForNextTickAsync())
            {
                await AutoSave();
            }
        }

        private static async Task AutoSave()
        {
            await SaveAllDataAsync();
        }

        public static async Task<bool> SaveAllDataAsync()
        {
            bool savedAll;
            savedAll = await SaveUserData();

            return savedAll;
        }

        public static async Task<bool> LoadAllDataAsync()
        {
            bool loadedAll = false;
            UserDataXML = await ObjectSerializer.DeserializeFromInternalAsync<UserDataXML>("user_dataset.xml");

            if(UserDataXML != null)
            {
                loadedAll = true;
            }

            return loadedAll;
        }

        public static async Task<bool> SaveUserData()
        {
            return await ObjectSerializer.SerializeToInternalAsync<UserDataXML>("user_dataset.xml", UserDataXML);
        }
        #endregion

        #region Additional Setter Getter
        public static void SetUserData(ObservableCollection<UserViewModel> userViewModels)
        {
            UserDataXML = ToUserDataXML(userViewModels);
        }

        public static ObservableCollection<UserViewModel> GetUserDataAsUserVMCollection()
        {
            if (UserDataXML != null)
                return ToUserViewModelCollection(UserDataXML);
            else
                return null;
        }
        #endregion

        #region Data Converter
        public static UserDataXML ToUserDataXML(ObservableCollection<UserViewModel> userViewModels)
        {
            UserDataXML userDataXML = new UserDataXML();
            foreach (UserViewModel user in userViewModels)
            {
                userDataXML.UserDataSets.Add(new UserData(user.Id, user.UserName, user.FirstName, user.LastName, user.Email));
            }

            return userDataXML;
        }

        public static ObservableCollection<UserViewModel> ToUserViewModelCollection(UserDataXML userDataXML)
        {
            ObservableCollection<UserViewModel> userViewModels = new ObservableCollection<UserViewModel>();
            foreach (UserData user in userDataXML.UserDataSets)
            {
                userViewModels.Add(new UserViewModel() { Id = user.Id, UserName = user.UserName, FirstName = user.FirstName, LastName = user.LastName, Email = user.Email });
            }
            return userViewModels;
        }
        #endregion
    }
}
