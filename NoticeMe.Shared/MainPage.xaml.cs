using Microsoft.UI.Xaml.Controls;
using NoticeMe.Data.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NoticeMe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel MainViewModel { get; } = new();

        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}
