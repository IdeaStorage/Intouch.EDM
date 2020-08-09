using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateUserInfoPage : ContentPage
    {
        private UpdateUserInfoViewModel viewModel;

        public UpdateUserInfoPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UpdateUserInfoViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
            {
                await viewModel.Init();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}