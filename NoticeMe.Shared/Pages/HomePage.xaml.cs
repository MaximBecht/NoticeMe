using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using NoticeMe.Data.DataModels;
using NoticeMe.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoticeMe.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomeViewModel HomeViewModel;

        public HomePage()
        {
            if (this.DataContext == null)
            {
                HomeViewModel = PageNavigator.HomeViewModel;
                this.DataContext = HomeViewModel;
            }

            this.InitializeComponent();

            if(HomeViewModel.IoTDevices.Count <= 0)
                Test();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (HomeViewModel == null)
            {
                HomeViewModel = (HomeViewModel)e.Parameter;
            }
        }

        public void Test()
        {
            HomeViewModel.IoTDevices.Add(new IoTDevice("Device", "123-456-789", "Keyboard Detector", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_A.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Justin", "666-666-666", "Keyboard Detector Pro", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_B.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Mama", "000-111-222", "Keyboard Detector", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_C.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Boromir", "690-069-690", "Smart Lamp", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_D.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Peter", "420-420-420", "Keyboard Detector", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_E.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Kevin", "777-777-777", "Mouse Detector", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_F.jpeg"))));
            HomeViewModel.IoTDevices.Add(new IoTDevice("Leander", "123-456-789", "Keyboard Detector", new BitmapImage(new Uri("ms-appx:///Assets/Images/TestPersons/person_G.jpeg"))));

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

            HomeViewModel.IoTDevices[0].Name= "Mad Max";

            SimulateChange(HomeViewModel.IoTDevices.ToList());
        }

        private async void SimulateChange(List<IoTDevice> iotDevices, int maxTimeBetweenUpdates = 150)
        {
            new System.Threading.Thread(async delegate () { await SimulateTestTask(iotDevices, maxTimeBetweenUpdates); }).Start();
        }

        private async Task SimulateTestTask(List<IoTDevice> iotDevices, int maxTimeBetweenUpdates)
        {
            Random rnd = new Random();
            IoTDevice iotDevice;
            while (true)
            {
                if (iotDevices.Count > 1)
                    iotDevice = iotDevices[rnd.Next(0, iotDevices.Count)];
                else if (iotDevices.Count == 1)
                    iotDevice = iotDevices[0];
                else
                    break;

                if(iotDevice.Status.Category == StatusCategory.Afk || iotDevice.Status.Category == StatusCategory.Offline)
                {
                    continue;
                }

                if (this.DispatcherQueue.HasThreadAccess)
                {
                    iotDevice.TypingSpeed = rnd.Next(45, 150);
                }
                else
                {
                    bool isQueued = this.DispatcherQueue.TryEnqueue(
                    Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal,
                    () => iotDevice.TypingSpeed = rnd.Next(45, 150));
                }

                System.Threading.Thread.Sleep(rnd.Next(50, maxTimeBetweenUpdates));
            }
        }
    }
}