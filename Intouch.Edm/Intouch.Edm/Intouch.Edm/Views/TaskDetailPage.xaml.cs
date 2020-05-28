using Intouch.Edm.ViewModels;
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

        private void TaskListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            var statusData = e.SelectedItem as HelperModel;
            ((ListView)sender).SelectedItem = null;

            var item = viewModel.StatusRecords.FirstOrDefault(x => x.Name == statusData.Name);
            if (item != null)
            {
                item.IsSelected = !item.IsSelected;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}