using Intouch.Edm.Models;

namespace Intouch.Edm.ViewModels
{
    public class AnnouncementDetailViewModel : BaseViewModel
    {
        private Announcement _announcement;

        public Announcement Announcement
        {
            get { return _announcement; }
            set
            {
                _announcement = value;
                OnPropertyChanged();
            }
        }

        public AnnouncementDetailViewModel(Announcement announcement)
        {
            Announcement = announcement;
        }
    }
}