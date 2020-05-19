using Intouch.Edm.Models;
using Intouch.Edm.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
        }

        private ICommand _signInCommand;

        public ICommand SignInCommand => _signInCommand
                ?? (_signInCommand = new Command(async () => await ExecuteSignInCommand()));

        private string _username;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                SetProperty(ref _username, value);
            }
        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        private async System.Threading.Tasks.Task ExecuteSignInCommand()
        {
            IsBusy = true;
            User user = new User
            {
                UserNameOrEmailAddress = Username != null ? Username.Trim() : "",
                Password = Password != null ? Password.Trim() : ""
            };

            if (string.IsNullOrWhiteSpace(user.UserNameOrEmailAddress) || string.IsNullOrWhiteSpace(user.Password))
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("UYARI", "Kullanıcı Adı ve Şifre girmelisiniz!", "TAMAM");
                return;
            }

            var response = await DataService.GetAuthTokenAsync(user);

            Helpers.Settings.Username = user.UserNameOrEmailAddress.Trim();
            Helpers.Settings.Password = user.Password.Trim();

            if (!response.Success)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("UYARI", "Kullanıcı Adı veya Şifrenizi yanlış girdiniz!", "TAMAM");
            }
            else
            {
                Helpers.Settings.SetTokenInformation(response);
                // set
                if (!string.IsNullOrEmpty(Helpers.Settings.FirebaseNotification))
                {
                    await DataService.SaveFirebaseTokenAsync(
                        new UserMobileAppTokenInput()
                        {
                            Token = Helpers.Settings.FirebaseNotification,
                            UserId = int.Parse(Helpers.Settings.UserId)
                        });
                }
                var responsePermission = await DataService.GetPermissionsAsync(Convert.ToInt32(Helpers.Settings.UserId));
                if (responsePermission.success)
                {
                    Helpers.Settings.SetPermission(responsePermission);
                    IsBusy = false;
                }

                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
        }
    }

    public class UserMobileAppTokenInput
    {
        public long UserId { get; set; }
        public string Token { get; set; }
    }
}