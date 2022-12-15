using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NoticeMe.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Storage;

namespace NoticeMe.Data.ViewModels
{


    public enum Theme
    {
        SystemDefault,
        Light,
        Dark,
        HighContrastBlack,
        HighContrastWhite,
        Default
    }
    public enum Font
    {
        AkzidenzGrotesk,
        Helvetica,
        Didot,
        Baskerville,
        GillSans,
        Bembo,
        Sabon,
        Georgia,
        NewsGothic,
        Myriad,
        Minion,
        MrsEaves,
        Garamond,
        Gotham,
        Futura,
        Bodoni,
        Arial,
        TimesNewRoman,
        Verdana,
        Rockwell,
        FranklinGothic,
        Univers,
        Frutiger,
        Avenir,
    }


    public partial class SettingsViewModel : INotifyPropertyChanged
    {
        private Theme _selectedTheme = Theme.Default;
        public Theme SelectedTheme 
        {
            get => _selectedTheme;
            set
            {
                if(value == Theme.Default)
                {
                    ChangeTheme(_selectedTheme, (false));
                    OnPropertyChanged("SelectedTheme");
                    OnPropertyChanged("DisplaySelectedTheme");
                }
                else if (_selectedTheme != value)
                {
                    ChangeTheme(value, _selectedTheme != Theme.Default);
                    _selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");
                    OnPropertyChanged("DisplaySelectedTheme");
                }
            }
        }

        public string DisplaySelectedTheme
        {
            get
            {
                return SelectedTheme.ToString();
            }
        }


        public SettingsViewModel() 
        {
            SetThemeOnStartup();
        }

        private void ChangeTheme(Theme theme, bool refresh)
        {
            switch (theme)
            {
                case Theme.Default: ChangeThemeSource("ms-appx:///Assets/Themes/LightTheme.xaml"); break;
                case Theme.Light: ChangeThemeSource("ms-appx:///Assets/Themes/LightTheme.xaml"); break;
                case Theme.Dark: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
                case Theme.HighContrastBlack: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
                case Theme.HighContrastWhite: ChangeThemeSource("ms-appx:///Assets/Themes/DarkTheme.xaml"); break;
            }

            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["AppTheme"] = theme.ToString();

            if (refresh)
            {
                RefreshUI();
            }
        }
        private void ChangeThemeSource(string source)
        {
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri(source);
        }
        private void ChangeFont(Font font)
        {

        }
        private void SetThemeOnStartup()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Theme requestedTheme = Theme.Default;
            if (localSettings.Values.ContainsKey("AppTheme"))
            {
                requestedTheme = ParseEnum<Theme>(localSettings.Values["AppTheme"]);
            }

            SelectedTheme = requestedTheme;
        }

        private void RefreshUI()
        {
            ((App)Application.Current).RootFrame.Navigate(typeof(MainPage));
        }
        private static T ParseEnum<T>(object value)
        {
            if (value == null)
                return default(T);
            return (T)Enum.Parse(typeof(T), value.ToString());
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
