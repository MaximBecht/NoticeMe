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
        private ApplicationTheme _selectedAppTheme = Application.Current.RequestedTheme;
        public ApplicationTheme SelectedAppTheme 
        {
            get => _selectedAppTheme;
            set
            {
                if(_selectedAppTheme != value) 
                {
                    _selectedAppTheme = value;
                    ChangeApplicationTheme(_selectedAppTheme);
                    OnPropertyChanged("SelectedTheme");
                    OnPropertyChanged("DisplaySelectedTheme");
                }
            }
        }

        public string DisplaySelectedTheme
        {
            get
            {
                return SelectedAppTheme.ToString();
            }
        }


        public SettingsViewModel() 
        {

        }

        private void ChangeApplicationTheme(ApplicationTheme requestedAppTheme)
        {
            ApplicationData.Current.LocalSettings.Values["AppTheme"] = (int)requestedAppTheme;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
