using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using NoticeMe.Data.ViewModels;

namespace NoticeMe.Pages
{
    public sealed partial class ProfilePage : Page
    {
        ProfileViewModel ProfileViewModel;
        public ProfilePage()
        {
            this.DataContext = ProfileViewModel;
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ProfileViewModel = (ProfileViewModel)e.Parameter;
            this.DataContext = ProfileViewModel;
        }
    }
}
