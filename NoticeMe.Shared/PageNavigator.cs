using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using NoticeMe.Data.ViewModels;
using NoticeMe.Pages;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.UI;

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

            switch (b.Name)
            {
                case "NavigateScoreboardBtn":
                    Navigate("Scoreboard", typeof(ScoreboardPage), b, new EntranceNavigationTransitionInfo());
                    break;
                case "NavigateProfileBtn":
                    Navigate("Profile", typeof(ProfilePage), b, new EntranceNavigationTransitionInfo());
                    break;
                case "NavigateHomeBtn":
                    Navigate("Home", typeof(HomePage), b, new EntranceNavigationTransitionInfo());
                    break;
                case "NavigateSettingsBtn":
                    Navigate("Settings", typeof(SettingsPage), b, new EntranceNavigationTransitionInfo());
                    break;
                case "NavigateAboutBtn":
                    Navigate("About", typeof(AboutPage), b, new EntranceNavigationTransitionInfo());
                    break;
                default:
                    Navigate("Home", typeof(HomePage), b, new EntranceNavigationTransitionInfo());
                    break;
            }
        }

        public static void Navigate(string pageTitle, Type pageType, Button navigationBtn = null, NavigationTransitionInfo themeTransitionInfo = null)
        {
            if (themeTransitionInfo == null)
                themeTransitionInfo = new EntranceNavigationTransitionInfo();

            _contentFrame.Navigate(pageType, GetViewModel(pageTitle), themeTransitionInfo);

            _lastTitle = _mainViewModel.GetCurrentPageTitle();
            _mainViewModel.ChangePageTitle(pageTitle);

            if (navigationBtn != null)
            {
                List<BitmapIcon> icon = new List<BitmapIcon>();
                List<TextBlock> text = new List<TextBlock>();
                FindChildren<BitmapIcon>(icon, navigationBtn);
                FindChildren<TextBlock>(text, navigationBtn);

                if (icon.Count > 0 && text.Count > 0)
                {
                    SetButtonVisualState(icon[0], text[0]);
                }
            }
        }
        public static void Navigate(string pageTitle, Type pageType, BitmapIcon icon, TextBlock text, NavigationTransitionInfo themeTransitionInfo = null)
        {
            Navigate(pageTitle, pageType);
            if (icon != null && text != null)
                SetButtonVisualState(icon, text);
        }

        private static void SetButtonVisualState(BitmapIcon icon, TextBlock text)
        {
            if (_lastActiveIcon != null && _lastActiveText != null)
            {
                var colorBrush = (SolidColorBrush)Application.Current.Resources["SystemColorDisabledTextBrush"];
                _lastActiveIcon.Foreground = colorBrush;
                _lastActiveText.Foreground = colorBrush;
            }

            _lastActiveIcon = icon;
            _lastActiveText = text;
            var color = (Color)Application.Current.Resources["SystemAccentColor"];
            SolidColorBrush brush = new SolidColorBrush(color);
            icon.Foreground = brush;
            text.Foreground = brush;
        }

        public static object GetViewModel(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Scoreboard": return null;
                case "Profile": return ProfileViewModel;
                case "Home": return _mainViewModel;
                case "Settings": return SettingsViewModel;
                case "About": return null;
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
