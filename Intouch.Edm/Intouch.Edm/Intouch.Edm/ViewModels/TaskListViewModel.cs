﻿using Intouch.Edm.Models;
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

        private string selectedFilter = "Hepsi";

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
            FilterOptions = new ObservableRangeCollection<string>
            {
                "Hepsi"
            };
            LoadTasksCommand = new Command(async () => await ExecuteLoadTasksCommand());
        }

        private void FilterItems()
        {
            TaskItems.ReplaceRange(AllTaskItems.Where(a => a.UserName == SelectedFilter || SelectedFilter == "Hepsi"));
        }

        private async Task ExecuteLoadTasksCommand()
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
                ClearFilterOptions(FilterOptions);
                FilterOptions.AddRange(AllTaskItems.Select(a => a.UserName).Distinct().ToList());
                FilterItems();
            }
            catch (System.Exception ex)
            {
                IsBusy = false;
                HandleException(ex, "Bağlantı sağlanırken hata oluştu.");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Hepsi seçeneğini çıkarmaması için break var...
        /// </summary>
        /// <param name="filterOptions"></param>
        public static void ClearFilterOptions(ObservableRangeCollection<string> filterOptions)
        {
            while (filterOptions.Count > 0)
            {
                if ((filterOptions.Count - 1) == 0)
                {
                    break;
                }
                filterOptions.RemoveAt(filterOptions.Count - 1);
            }
        }
    }
}