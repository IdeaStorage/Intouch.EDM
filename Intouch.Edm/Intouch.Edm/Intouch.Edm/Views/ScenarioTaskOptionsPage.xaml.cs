using Intouch.Edm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenarioTaskOptionsPage : ContentPage
    {
        private ScenarioTaskOptionsViewModel viewModel;
        public ScenarioTaskOptionsPage(ScenarioTaskOptionsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
        }
    }
}