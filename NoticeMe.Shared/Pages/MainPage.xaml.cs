using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NoticeMe.Data.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NoticeMe.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; } = new();
        //RestApiTests restApiTests;

        public MainPage()
        {
            this.InitializeComponent();

            //restApiTests = new RestApiTests();
            //restApiTests.CreateHttpClient();

            PageNavigator.Init(ContentFrame, MainViewModel);
            PageNavigator.Navigate("Home", typeof(HomePage));
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigationButton_Click(sender, e);
        }


        /// <summary>
        /// Just for testing RestApi
        /// </summary>
        //public async void TestRestApiButton()
        //{
        //    await restApiTests.ProcessRepositories();
        //}


        /// <summary>
        /// Just as a reminder of possibilities
        /// </summary>
        /// <returns></returns>
        //public static string CurrentTheme()
        //{
        //    var isDark = Application.Current.RequestedTheme == ApplicationTheme.Dark;

        //    if (isDark)
        //        return "Dark";

        //    return "Light";
        //}

    }
}
