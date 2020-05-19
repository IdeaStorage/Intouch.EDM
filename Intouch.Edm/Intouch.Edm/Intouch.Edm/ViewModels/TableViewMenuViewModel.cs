using Intouch.Edm.Helpers;
using Intouch.Edm.Models;
using Intouch.Edm.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class TableViewMenuViewModel : BaseViewModel
    {
        public TableViewMenuViewModel()
        {
        }

        private ICommand _newNotificationButton;

        public ICommand NewNotificationClicked => _newNotificationButton
        ?? (_newNotificationButton = new CommandHandler(param => NavigateNewNotification(param), true));

        private async Task NavigateNewNotification(object param)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewScenarioPage(param));
        }

        internal async Task Init()
        {
            // Method intentionally left empty.
        }

        private ICommand _announcementButton;

        public ICommand AnnouncementClicked => _announcementButton
        ?? (_announcementButton = new CommandHandler(param => NavigateAnnouncementList(), true));

        private async Task NavigateAnnouncementList()
        {
            await DataService.SetReadNotifications();
            await Application.Current.MainPage.Navigation.PushAsync(new MainPage((int)TabPageEnums.AnnouncementListPage));
        }
    }
}