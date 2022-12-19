using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using NoticeMe.Data.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.UI.Xaml;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoticeMe.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsViewModel SettingsViewModel;
        public SettingsPage()
        {
            if(this.DataContext == null)
            {
                SettingsViewModel = PageNavigator.SettingsViewModel;
                this.DataContext = SettingsViewModel;
            }

            this.InitializeComponent();

            LanguageComboBox.SelectedIndex = SettingsViewModel.SelectedLanguageIndex;
            ThemeComboBox.SelectedIndex = SettingsViewModel.SelectedAppThemeIndex;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (SettingsViewModel == null) 
            {
                SettingsViewModel = (SettingsViewModel)e.Parameter;
            }
        }

        private void LanguageDropDown_Closed(object sender, object e)
        {
            SettingsViewModel.SelectedLanguageIndex = LanguageComboBox.SelectedIndex;
        }

        private void ThemeDropDown_Closed(object sender, object e)
        {
            SettingsViewModel.SelectedAppThemeIndex = ThemeComboBox.SelectedIndex;
        }
    }
}
