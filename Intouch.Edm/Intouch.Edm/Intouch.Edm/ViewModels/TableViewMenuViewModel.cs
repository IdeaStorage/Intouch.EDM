using Intouch.Edm.Helpers;
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
        }
    }
}