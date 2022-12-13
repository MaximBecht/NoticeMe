using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using NoticeMe.Data.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using NoticeMe.Data.ViewModels;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace NoticeMe.Pages
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePageViewModel HomePageViewModel;

        public HomePage()
        {
            HomePageViewModel = new HomePageViewModel();
            this.DataContext = HomePageViewModel;
            this.InitializeComponent();

            Test();
        }

        public void Test()
        {
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Device", "123-456-789", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("GabeN's Spy", "666-666-666", "Keyboard Detector Pro", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("My Device", "000-111-222", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("The Lamp", "690-069-690", "Smart Lamp", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Little Bro", "420-420-420", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("My Mouse", "777-777-777", "Mouse Detector", null));

            HomePageViewModel.IoTDevices.Add(new IoTDevice("Device 2", "123-456-789", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Test", "666-666-666", "Keyboard Detector Pro", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Hello World", "000-111-222", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Wow", "690-069-690", "Smart Lamp", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("LOL", "420-420-420", "Keyboard Detector", null));
            HomePageViewModel.IoTDevices.Add(new IoTDevice("Smash It", "777-777-777", "Mouse Detector", null));
        }
    }
}
