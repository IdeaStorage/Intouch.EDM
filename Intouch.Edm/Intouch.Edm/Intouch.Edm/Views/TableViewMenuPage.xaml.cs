using Intouch.Edm.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TableViewMenuPage : ContentPage
    {
        TableViewMenuViewModel viewModel;
        public TableViewMenuPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = viewModel = new TableViewMenuViewModel();
        }
       

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Initialize TableViewMenuModel
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