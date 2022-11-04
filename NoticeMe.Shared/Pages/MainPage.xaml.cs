using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NoticeMe.Data.ViewModels;

namespace NoticeMe.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; } = new();

        public MainPage()
        {
            this.InitializeComponent();

            PageNavigator.Init(ContentFrame, MainViewModel);
            PageNavigator.Navigate("Home", typeof(HomePage), NavigateHomeBtn);
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigationButton_Click(sender, e);
        }

        /// <summary>
        /// Just as a reminder of possibilities
        /// </summary>
        /// <returns></returns>
        public static string CurrentTheme()
        {
            var isDark = Application.Current.RequestedTheme == ApplicationTheme.Dark;

            if (isDark)
                return "Dark";

            return "Light";
        }
    }
}
