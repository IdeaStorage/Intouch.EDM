

using Intouch.Edm.Services;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            GetUnreadAnnouncementCountAsync();
            GetTabPageVisible();
            
        }

        private int _unreadCount;

        public int UnreadCount
        {
            get
            {
                return _unreadCount;
            }
            set
            {
                SetProperty(ref _unreadCount, value);
            }
        }

        private bool _isVisibleTaskList;

        public bool IsVisibleTaskList
        {
            get
            {
                return _isVisibleTaskList;
            }
            set
            {
                SetProperty(ref _isVisibleTaskList, value);
            }
        }

        private bool _isVisibleAdkList;

        public bool IsVisibleAdkList
        {
            get
            {
                return _isVisibleAdkList;
            }
            set
            {
                SetProperty(ref _isVisibleAdkList, value);
            }
        }

        private async void GetUnreadAnnouncementCountAsync()
        {
            var announcementCount = await DataService.GetAnnouncementsCountAsync();
            int unreadCount = 0;
            if (announcementCount?.result != null)
            {
                unreadCount = announcementCount.result;
            }
            UnreadCount = unreadCount;
        }

        private void GetTabPageVisible()
        {
            IsVisibleTaskList = Helpers.Settings.isTasksAuthorize == "1";
            IsVisibleAdkList = Helpers.Settings.isADKAuthorize == "1";
        }
    }
}