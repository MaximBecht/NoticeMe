using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using NoticeMe.Data.DataModels;
using NoticeMe.Data.ViewModels;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoticeMe.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class ScoreboardPage : Page
    {
        public ScoreboardViewModel ScoreboardViewModel;

        public HomeViewModel HomeViewModel
        {
            get { return ScoreboardViewModel.HomeViewModel; }
        }

        public ScoreboardPage()
        {
            if (this.DataContext == null)
            {
                ScoreboardViewModel = PageNavigator.ScoreboardViewModel;
                ScoreboardViewModel.HomeViewModel = PageNavigator.HomeViewModel;
                this.DataContext = ScoreboardViewModel;
            }

            this.InitializeComponent();

            if (HomeViewModel.IoTDevices.Count <= 0)
                Test();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (HomeViewModel == null)
            {
                ScoreboardViewModel.HomeViewModel = (HomeViewModel)e.Parameter;
            }
        }

        public void Test()
        {
            HomeViewModel.IoTDevices.Add(new IoTDevice("Device", "123-456-789", "Keyboard Detector", null));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Justin", "666-666-666", "Keyboard Detector Pro", null));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Mama", "000-111-222", "Keyboard Detector", null));
            HomeViewModel.IoTDevices.Add(new IoTDevice("The Länd", "690-069-690", "Smart Lamp", null));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Emma", "420-420-420", "Keyboard Detector", null));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Device", "777-777-777", "Mouse Detector", null));

            HomeViewModel.IoTDevices[0].Status.Category = StatusCategory.Online;
            HomeViewModel.IoTDevices[1].Status.Category = StatusCategory.Afk;
            HomeViewModel.IoTDevices[2].Status.Category = StatusCategory.Online;
            HomeViewModel.IoTDevices[3].Status.Category = StatusCategory.Online;

            HomeViewModel.IoTDevices[0].Keystrokes = 12567;
            HomeViewModel.IoTDevices[1].Keystrokes = 8034;
            HomeViewModel.IoTDevices[2].Keystrokes = 413;
            HomeViewModel.IoTDevices[3].Keystrokes = 321;
            HomeViewModel.IoTDevices[4].Keystrokes = 150;
            HomeViewModel.IoTDevices[5].Keystrokes = 73;

            HomeViewModel.IoTDevices[0].TypingSpeed = 176;
            HomeViewModel.IoTDevices[1].TypingSpeed = 0;
            HomeViewModel.IoTDevices[2].TypingSpeed = 15;
            HomeViewModel.IoTDevices[3].TypingSpeed = 13;
            HomeViewModel.IoTDevices[4].TypingSpeed = 0;
            HomeViewModel.IoTDevices[5].TypingSpeed = 0;

            HomeViewModel.IoTDevices[0].Name = "Mad Max";
        }
    }
}