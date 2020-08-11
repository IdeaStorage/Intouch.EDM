using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.TaskOptionDto;
using Intouch.Edm.ViewModels;
using System.Linq;
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

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var label = (Label)sender;
            var statusData = label.BindingContext as HelperModel;

            var options = viewModel.StatusRecords.Where(x => x.UserId == statusData.UserId).ToList();

            TaskItem taskItem = new TaskItem();
            taskItem.Options = new RootObject();
            taskItem.Options.result = new Models.Dtos.TaskOptionDto.Result();
            taskItem.Options.result.items = new System.Collections.Generic.List<Item>();

            foreach (var option in options)
            {
                Item checkedOptionItem = new Item();
                checkedOptionItem.checkedOption.id = option.Id;
                checkedOptionItem.checkedOption.text = option.Name;
                checkedOptionItem.checkedOption.userId = option.UserId;
                checkedOptionItem.checkedOption.userFullName = option.UserFullName;
                checkedOptionItem.checkedOption.taskUserPhone = option.UserPhone;
                checkedOptionItem.checkedOption.completed = option.IsSelected;
                taskItem.Options.result.items.Add(checkedOptionItem);
            }
            taskItem.Options.result.totalCount = options.Count;
            await Application.Current.MainPage.Navigation.PushAsync(new UserTaskOptionsPage(new UserTaskOptionsViewModel(taskItem)));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}