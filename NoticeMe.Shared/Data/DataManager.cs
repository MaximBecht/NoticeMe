using NoticeMe.Data.DataModels;
using NoticeMe.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoticeMe.Data
{
    public static class DataManager
    {
        public static UserDataXML UserDataXML { get; set; }

        #region FileHandling
        public static async void InitAutoSaver()
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(10));

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
            return ToUserViewModelCollection(UserDataXML);
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
