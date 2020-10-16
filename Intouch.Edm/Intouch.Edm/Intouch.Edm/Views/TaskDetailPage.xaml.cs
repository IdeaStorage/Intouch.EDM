using Intouch.Edm.ViewModels;
using Plugin.InputKit.Shared.Controls;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        private TaskDetailViewModel viewModel;

        public TaskDetailPage(TaskDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void CheckBox_CheckChanged(object sender, System.EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox == null)
            {
                return;
            }
            var statusData = checkbox.BindingContext as HelperModel;

            var item = viewModel.StatusRecords.FirstOrDefault(x => x.Id == statusData.Id);
            if (item != null)
            {
                item.IsSelected = checkbox.IsChecked;
            }
        }

        private void Logout_Clicked(object sender, System.EventArgs e)
        {
            viewModel.Logout();
        }
    }
}