using Intouch.Edm.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class AnnouncementViewModel : BaseViewModel
    {
        private string _announcementTitle;

        public string AnnouncementTitle
        {
            get
            {
                return _announcementTitle;
            }
            set
            {
                SetProperty(ref _announcementTitle, value);
            }
        }

        private string _announcementDescription;

        public string AnnouncementDescription
        {
            get
            {
                return _announcementDescription;
            }
            set
            {
                SetProperty(ref _announcementDescription, value);
            }
        }

        private ICommand _newAnnouncementCommand;

        public ICommand NewAnnouncementButtonClicked => _newAnnouncementCommand
                ?? (_newAnnouncementCommand = new Command(async () => await NewAnnouncementClicked()));

        private async System.Threading.Tasks.Task NewAnnouncementClicked()
        {
            Models.Dtos.CreateAnnouncementDto.CreateAnnouncementDto createAnnouncement = new Models.Dtos.CreateAnnouncementDto.CreateAnnouncementDto { title = AnnouncementTitle, text = AnnouncementDescription };
            var createAnnouncementResult = await DataService.CreateAnnouncementAsync(createAnnouncement);
            if (createAnnouncementResult)
            {
                await Application.Current.MainPage.DisplayAlert("İşlem Başarılı", $"{AnnouncementTitle} başlıklı duyuru gönderilmiştir", "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
        }
    }
}