using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementDetailPage : ContentPage
    {
        private AnnouncementDetailViewModel viewModel;

        public AnnouncementDetailPage(AnnouncementDetailViewModel _viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel = _viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            viewModel.Logout();
        }
    }
}