using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using NoticeMe.Data.ViewModels;

namespace NoticeMe.Pages
{
    public sealed partial class ProfilePage : Page
    {
        public ProfileViewModel ProfileViewModel;
        public ProfilePage()
        {
            ProfileViewModel = PageNavigator.ProfileViewModel;
            this.DataContext = ProfileViewModel;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ProfileViewModel = (ProfileViewModel)e.Parameter;
        }
    }
}