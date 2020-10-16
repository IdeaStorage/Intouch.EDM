using Intouch.Edm.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewScenarioPage : ContentPage
    {
        private NewScenarioViewModel viewModel;

        public NewScenarioPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewScenarioViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel != null)
            {
                await viewModel.Init();
            }
        }

        public NewScenarioPage(object param)
        {
            InitializeComponent();
            BindingContext = viewModel = new NewScenarioViewModel(int.Parse(param.ToString()));
        }

        protected override bool OnBackButtonPressed()
        {
            viewModel.IsBusy = false;
            return true;
        }

        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            viewModel.Logout();
        }
    }
}