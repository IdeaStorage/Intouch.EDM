using Intouch.Edm.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class ForgetPasswordPageModel : BaseViewModel
    {
        private string _mail;
        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                SetProperty(ref _mail, value);
            }
        }
        ICommand _sendEmailAdressCommand;

        public ICommand SendEmailAdressButtonClicked => _sendEmailAdressCommand
                ?? (_sendEmailAdressCommand = new Command(async () => await SendEmailAdressClicked()));

        async System.Threading.Tasks.Task SendEmailAdressClicked()
        {
            await Application.Current.MainPage.DisplayAlert("İşlem Başarılı", $"{Mail} adresine bilgi gönderilecektir.", "TAMAM");
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
