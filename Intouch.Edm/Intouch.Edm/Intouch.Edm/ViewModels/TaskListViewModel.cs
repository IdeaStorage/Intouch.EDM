using Intouch.Edm.Models;
using Intouch.Edm.Services;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<TaskItem> TaskItems { get; set; }
        public ObservableRangeCollection<TaskItem> AllTaskItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }

        string selectedFilter = "Hepsi";
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                if (SetProperty(ref selectedFilter, value))
                    FilterItems();
            }
        }
        public Command LoadTasksCommand { get; set; }

        public TaskListViewModel()
        {
            TaskItems = new ObservableRangeCollection<TaskItem>();
            AllTaskItems = new ObservableRangeCollection<TaskItem>();
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
            FilterOptions = new ObservableRangeCollection<string>
            {
                "Hepsi"
            };
        }

        void FilterItems()
        {
            TaskItems.ReplaceRange(AllTaskItems.Where(a => a.UserName == SelectedFilter || SelectedFilter == "Hepsi"));
        }

        async Task ExecuteLoadTasksCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                TaskItems.Clear();
                var taskItems = await DataService.GetTasksAsync(Convert.ToInt32(Helpers.Settings.UserId));
                var records = taskItems.result.items.OrderByDescending(x => x.userTask.assignmentTime).ToList();
                foreach (var taskItem in records)
                {
                    TaskItem item = TaskService.GetTask(taskItem);
                    TaskItems.Add(item);
                }
                AllTaskItems.ReplaceRange(TaskItems);
                FilterOptions.AddRange(AllTaskItems.Select(a => a.UserName).Distinct().ToList());
                FilterItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}