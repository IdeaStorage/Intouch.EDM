using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableViewMenuPage : ContentPage
    {
        private TableViewMenuViewModel viewModel;

        public TableViewMenuPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = viewModel = new TableViewMenuViewModel();
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