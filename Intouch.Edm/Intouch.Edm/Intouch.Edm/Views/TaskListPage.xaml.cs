using Intouch.Edm.Models;
using Intouch.Edm.Services;
using Intouch.Edm.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Intouch.Edm.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskListPage : ContentPage
    {
        private TaskListViewModel viewModel;
        private DataService dataService;

        public TaskListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new TaskListViewModel();
            dataService = new DataService(new Uri("http://edm.intouch.istanbul"), Helpers.Settings.AuthenticationToken);
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is TaskItem taskItem))
            {
                return;
            }

            var options = await dataService.GetTaskOptionsAsync(taskItem.Id);
            taskItem.Options = options;
            var scenarioDto = await dataService.GetScenarioDetailAsync(taskItem.ScenarioId);
            taskItem.ScenarioDto = scenarioDto;

            await Application.Current.MainPage.Navigation.PushAsync(new TaskDetailPage(new TaskDetailViewModel(taskItem)));

            TaskListView.SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.TaskItems.Count == 0)
            {
                viewModel.LoadTasksCommand.Execute(null);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}