using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using NoticeMe.Data.ViewModels;
using NoticeMe.Services.Particle;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NoticeMe.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; } = new();
        RestApiTests restApiTests;

        public MainPage()
        {
            this.InitializeComponent();

            restApiTests = new RestApiTests();
            restApiTests.CreateHttpClient();

            ContentFrame.Navigate(typeof(HomePage));
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            switch (b.Name)
            {
                case "NavigateScoreboardBtn": 
                    ContentFrame.Navigate(typeof(ScoreboardPage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("Scoreboard"); break;
                case "NavigateProfileBtn": 
                    ContentFrame.Navigate(typeof(ProfilePage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("Profile"); break;
                case "NavigateHomeBtn": 
                    ContentFrame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("Home"); break;
                case "NavigateSettingsBtn": 
                    ContentFrame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("Settings"); break;
                case "NavigateAboutBtn": 
                    ContentFrame.Navigate(typeof(AboutPage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("About"); break;
                default: 
                    ContentFrame.Navigate(typeof(HomePage), null, new EntranceNavigationTransitionInfo());
                    MainViewModel.ChangePageTitle("Home"); break;
            }
        }


        /// <summary>
        /// Just for testing RestApi
        /// </summary>
        public async void TestRestApiButton()
        {
            await restApiTests.ProcessRepositories();
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
