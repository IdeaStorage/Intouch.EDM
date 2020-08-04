using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTaskOptionsPage : ContentPage
    {
        private UserTaskOptionsViewModel viewModel;
        public UserTaskOptionsPage(UserTaskOptionsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}