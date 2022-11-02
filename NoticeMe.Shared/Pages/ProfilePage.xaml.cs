using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using NoticeMe.Data.ViewModels;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace NoticeMe.Pages
{
    public sealed partial class ProfilePage : Page
    {
        ProfileViewModel ProfileViewModel { get; } = new();
        public ProfilePage()
        {
            this.DataContext = ProfileViewModel;
            this.InitializeComponent();
        }

        private async void OpenFilePicker()
        {
#if WINDOWS
            //Get the Window's HWND (suboptimal solution to just create a new Window -> should close before application closes, otherwise theres some process still running in the background LULE)
            var window = new Microsoft.UI.Xaml.Window(); // Normally should get current window of page or application, but is always null, even with dispatcher => Sadge, why so over complicated for something that simple, Microsoft?     nice suggestion btw -> var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this); PEPE Laugh
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

            // trying with "WinRT.Interop.WindowNative.GetWindowHandle(this)" in normal dispatcher may work? But i dont try that now, way to disappointed
#endif

            var fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");
            fileOpenPicker.FileTypeFilter.Add(".webp");

#if WINDOWS
            WinRT.Interop.InitializeWithWindow.Initialize(fileOpenPicker, hwnd);
#endif

            StorageFile pickedFile = await fileOpenPicker.PickSingleFileAsync();

            if (pickedFile != null)
            {
                using (IRandomAccessStream fileStream = await pickedFile.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);

                    ProfileViewModel.EditedProfileImage = bitmapImage;
                }
            }
            else
            {
                // No file was picked or the dialog was cancelled.
            }

#if WINDOWS
            window.Close();
#endif
        }
    }
}
