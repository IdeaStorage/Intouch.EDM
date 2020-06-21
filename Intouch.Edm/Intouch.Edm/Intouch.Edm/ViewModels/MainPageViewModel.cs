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

        private int? _unreadCount;
        public string Count => _unreadCount <= 0 ? string.Empty : _unreadCount.ToString();

        public int? UnreadCount
        {
            get => _unreadCount;
            set
            {
                _unreadCount = value;
                OnPropertyChanged(nameof(UnreadCount));
                OnPropertyChanged(nameof(Count));
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
            if (announcementCount?.result != null && announcementCount.result > 0)
            {
                UnreadCount = announcementCount.result;
            }
        }

        private void GetTabPageVisible()
        {
            IsVisibleTaskList = Helpers.Settings.isTasksAuthorize == "1";
            IsVisibleAdkList = Helpers.Settings.isADKAuthorize == "1";
        }
    }
}