using Intouch.Edm.ViewModels;
using Intouch.Edm.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementList : ContentPage
    {
        AnnouncementListViewModel viewModel;
        public AnnouncementList()
        {
            InitializeComponent();
            BindingContext = viewModel = new AnnouncementListViewModel();
        }        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.AnnouncementItems.Count == 0)
            {
                viewModel.LoadAnnouncementsCommand.Execute(null);
            }
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AnnouncementPage(new AnnouncementViewModel()));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}