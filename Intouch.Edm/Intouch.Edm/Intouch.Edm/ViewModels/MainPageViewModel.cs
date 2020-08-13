namespace Intouch.Edm.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            GetUnreadAnnouncementCountAsync();
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

        private async void GetUnreadAnnouncementCountAsync()
        {
            var announcementCount = await DataService.GetAnnouncementsCountAsync();
            if (announcementCount?.result != null && announcementCount.result > 0)
            {
                UnreadCount = announcementCount.result;
            }
        }
    }
}