using Intouch.Edm.Models;
using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AnnouncementList : ContentPage
    {
        private AnnouncementListViewModel viewModel;

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

        private void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            (BindingContext as AnnouncementListViewModel).LoadMoreItems(e.Item as Announcement);
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AnnouncementPage(new AnnouncementViewModel()));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Announcement announcement))
            {
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new AnnouncementDetailPage(new AnnouncementDetailViewModel(announcement)));

            AnnouncementListView.SelectedItem = null;
        }
    }
}