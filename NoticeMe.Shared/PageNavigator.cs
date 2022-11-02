using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using NoticeMe.Data.ViewModels;
using NoticeMe.Pages;
using System;

namespace NoticeMe
{
    public static class PageNavigator
    {
        private static Frame _contentFrame;
        private static MainViewModel _mainViewModel;
        private static ProfileViewModel _profileViewModel;

        private static string _lastTitle;

        public static void Init(Frame contentFrame, MainViewModel mainViewModel)
        {
            _contentFrame = contentFrame;
            _mainViewModel = mainViewModel;

            _profileViewModel = new ProfileViewModel();
        }

        public static void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            _lastTitle = _mainViewModel.GetCurrentPageTitle();

            switch (b.Name)
            {
                case "NavigateScoreboardBtn":
                    _contentFrame.Navigate(typeof(ScoreboardPage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Scoreboard"); break;
                case "NavigateProfileBtn":
                    _contentFrame.Navigate(typeof(ProfilePage), _profileViewModel, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Profile"); break;
                case "NavigateHomeBtn":
                    _contentFrame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Home"); break;
                case "NavigateSettingsBtn":
                    _contentFrame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Settings"); break;
                case "NavigateAboutBtn":
                    _contentFrame.Navigate(typeof(AboutPage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("About"); break;
                default:
                    _contentFrame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Home"); break;
            }
        }

        public static void Navigate(string pageTitle, Type pageType, NavigationTransitionInfo themeTransitionInfo = null)
        {
            if (themeTransitionInfo == null)
                themeTransitionInfo = new EntranceNavigationTransitionInfo();

            _contentFrame.Navigate(pageType, GetViewModel(pageTitle), themeTransitionInfo);

            _lastTitle = _mainViewModel.GetCurrentPageTitle();
            _mainViewModel.ChangePageTitle(pageTitle);
        }

        public static object GetViewModel(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Home": return _mainViewModel;
                case "Profile":
                case "Edit Profile": return _profileViewModel;
                default: return null;
            }
        }

        public static bool TryNavigateBack()
        {
            if (_contentFrame.CanGoBack)
            {
                _contentFrame.GoBack();
                _mainViewModel.ChangePageTitle(_lastTitle);
                return true;
            }
            return false;
        }
    }
}
