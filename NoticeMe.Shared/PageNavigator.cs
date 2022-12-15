using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using NoticeMe.Data.ViewModels;
using NoticeMe.Pages;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NoticeMe
{
    public static class PageNavigator
    {
        private static Frame _contentFrame;
        private static MainViewModel _mainViewModel;

        private static ProfileViewModel _profileViewModel = new();
        private static HomeViewModel _homeViewModel = new();
        private static SettingsViewModel _settingsViewModel = new();


        private static string _lastTitle;
        private static BitmapIcon _lastActiveIcon;
        private static TextBlock _lastActiveText;

        public static ProfileViewModel ProfileViewModel { get => _profileViewModel; set => _profileViewModel = value; }
        public static HomeViewModel HomeViewModel { get => _homeViewModel; set => _homeViewModel = value; }
        public static SettingsViewModel SettingsViewModel { get => _settingsViewModel; set => _settingsViewModel = value; }

        public static void Init(Frame contentFrame, MainViewModel mainViewModel)
        {
            _contentFrame = contentFrame;
            _mainViewModel = mainViewModel;
        }

        public static void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

            _lastTitle = _mainViewModel.GetCurrentPageTitle();

            switch (b.Name)
            {
                case "NavigateScoreboardBtn":
                    _contentFrame.Navigate(typeof(ScoreboardPage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Scoreboard");
                    break;
                case "NavigateProfileBtn":
                    _contentFrame.Navigate(typeof(ProfilePage), ProfileViewModel, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Profile"); 
                    break;
                case "NavigateHomeBtn":
                    _contentFrame.Navigate(typeof(HomePage), HomeViewModel, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Home"); 
                    break;
                case "NavigateSettingsBtn":
                    _contentFrame.Navigate(typeof(SettingsPage), SettingsViewModel, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Settings"); 
                    break;
                case "NavigateAboutBtn":
                    _contentFrame.Navigate(typeof(AboutPage), null, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("About"); 
                    break;
                default:
                    _contentFrame.Navigate(typeof(HomePage), HomeViewModel, new EntranceNavigationTransitionInfo());
                    _mainViewModel.ChangePageTitle("Home"); 
                    break;
            }

            SetButtonVisualState(b);
        }

        public static void Navigate(string pageTitle, Type pageType, Button navigationBtn = null, NavigationTransitionInfo themeTransitionInfo = null)
        {
            if (themeTransitionInfo == null)
                themeTransitionInfo = new EntranceNavigationTransitionInfo();

            _contentFrame.Navigate(pageType, GetViewModel(pageTitle), themeTransitionInfo);

            _lastTitle = _mainViewModel.GetCurrentPageTitle();
            _mainViewModel.ChangePageTitle(pageTitle);

            if (navigationBtn != null)
                SetButtonVisualState(navigationBtn);
        }

        private static void SetButtonVisualState(Button b)
        {
            List<BitmapIcon> icon = new List<BitmapIcon>();
            List<TextBlock> text = new List<TextBlock>();
            FindChildren<BitmapIcon>(icon, b);
            FindChildren<TextBlock>(text, b);

            if (_lastActiveIcon != null)
            {
                _lastActiveIcon.Foreground = Application.Current.Resources["NoticeMe_DisabledButtonIconColorBrush"] as Brush;
                _lastActiveText.Foreground = Application.Current.Resources["NoticeMe_DisabledTextColorBrush"] as Brush;
            }
            if (icon.Count > 0 && text.Count > 0)
            {
                _lastActiveIcon = icon[0];
                _lastActiveText = text[0];
                icon[0].Foreground = Application.Current.Resources["NoticeMe_SelectedButtonIconColorBrush"] as Brush;
                text[0].Foreground = Application.Current.Resources["NoticeMe_SelectedTextColorBrush"] as Brush;
            }
        }

        public static object GetViewModel(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Home": return _mainViewModel;
                case "Profile":
                case "Edit Profile": return ProfileViewModel;
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


        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
        where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }
    }
}
