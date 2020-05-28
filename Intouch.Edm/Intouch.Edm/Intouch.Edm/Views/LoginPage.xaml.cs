using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ForgetPasswordPage(new ForgetPasswordPageModel()));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}