using Microsoft.UI.Xaml.Controls;
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

        public async void TestRestApiButton()
        {
            await restApiTests.ProcessRepositories();
        }

    }
}
