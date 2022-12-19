using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using NoticeMe.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
                    OnPropertyChanged("DisplaySelectedTheme");
                }
            }
        }

        public string DisplaySelectedTheme
        {
            get
            {
                return SelectedAppThemeIndex.ToString();
            }
        }


        public SettingsViewModel() 
        {
            SelectedAppThemeIndex = (int)ApplicationData.Current.LocalSettings.Values["AppTheme"];
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
