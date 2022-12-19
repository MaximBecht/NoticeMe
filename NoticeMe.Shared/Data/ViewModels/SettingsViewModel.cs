using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Documents;
using NoticeMe.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Globalization;
using Windows.Storage;

namespace NoticeMe.Data.ViewModels
{
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
        private int _selectedAppThemeIndex;
        public int SelectedAppThemeIndex 
        {
            get => _selectedAppThemeIndex;
            set
            {
                if(_selectedAppThemeIndex != value) 
                {
                    _selectedAppThemeIndex = value;
                    ChangeApplicationTheme(_selectedAppThemeIndex);
                    OnPropertyChanged("SelectedTheme");
                }
            }
        }
        private int _selecteLanguageIndex;
        public int SelectedLanguageIndex
        {
            get => _selecteLanguageIndex;
            set
            {
                if (_selecteLanguageIndex != value)
                {
                    _selecteLanguageIndex = value;
                    ChangeLanguage(_selecteLanguageIndex);
                    OnPropertyChanged("SelectedLanguageIndex");
                }
            }
        }

        //public int DisplaySelectedLanguage
        //{
        //    get
        //    {
        //        return SelectedAppThemeIndex;
        //    }
        //}

        //public int DisplaySelectedTheme
        //{
        //    get
        //    {
        //        return SelectedAppThemeIndex;
        //    }
        //}


        public SettingsViewModel() 
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("AppTheme"))
            {
                SelectedAppThemeIndex = (int)ApplicationData.Current.LocalSettings.Values["AppTheme"];
            }
            else
            {
                SelectedAppThemeIndex = 2;
            }

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("LanguageIndex"))
            {
                SelectedLanguageIndex = (int)ApplicationData.Current.LocalSettings.Values["LanguageIndex"];
            }
            else
            {
                SelectedLanguageIndex = 0;
            }
        }


        private void ChangeLanguage(int requestedLanguageIndex)
        {
            switch (requestedLanguageIndex)
            {
                case 0: ApplicationLanguages.PrimaryLanguageOverride = "en"; break;
                case 1: ApplicationLanguages.PrimaryLanguageOverride = "de-DE"; break;
                default: ApplicationLanguages.PrimaryLanguageOverride = "en"; break;
            }
            ApplicationData.Current.LocalSettings.Values["LanguageIndex"] = requestedLanguageIndex;
        }
        private void ChangeApplicationTheme(int requestedAppThemeIndex)
        {
            ApplicationData.Current.LocalSettings.Values["AppTheme"] = requestedAppThemeIndex;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
