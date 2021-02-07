using Intouch.Edm.Models;
using Intouch.Edm.Services;
using Intouch.Edm.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private TaskItem _task;

        public TaskItem Task
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }

        public TaskDetailViewModel(TaskItem taskItem = null)
        {
            Task = taskItem;
            TaskDetailLists = new ObservableCollection<TaskDetail>();
            RefreshStatusRecords(taskItem);

            taskItem.Scenario = new Scenario
            {
                Id = taskItem.ScenarioDto.result.scenario.id,
                Source = taskItem.ScenarioDto.result.sourceName,
                Event = taskItem.ScenarioDto.result.eventTypeName,
                ImpactArea = taskItem.ScenarioDto.result.impactAreaName,
                Site = taskItem.ScenarioDto.result.siteName,
                Subject = taskItem.ScenarioDto.result.scenario.subjectType == 1 ? "Acil Durum" : "İş Sürekliliği",
                PictureUrl = taskItem.ScenarioDto.result.scenario.pictureUrl
            };
        }

        public void RefreshStatusRecords(TaskItem taskItem)
        {
            StatusRecords = new ObservableCollection<HelperModel>();

            foreach (var item in taskItem.Options.result.items)
            {
                StatusRecords.Add(new HelperModel
                {
                    Name = item.checkedOption.text,
                    Id = item.checkedOption.id,
                    IsSelected = item.checkedOption.completed
                });
            }
        }

        private ObservableCollection<HelperModel> _statusRecords;

        public ObservableCollection<HelperModel> StatusRecords
        {
            get { return _statusRecords; }
            set
            {
                _statusRecords = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TaskDetail> _taskDetailLists;

        public ObservableCollection<TaskDetail> TaskDetailLists
        {
            get { return _taskDetailLists; }
            set
            {
                _taskDetailLists = value;
                OnPropertyChanged();
            }
        }

        private ICommand _saveCommand;

        public ICommand SaveClicked => _saveCommand
                ?? (_saveCommand = new Command(async () => await ExecuteSaveClicked()));

        private async System.Threading.Tasks.Task ExecuteSaveClicked()
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;
                Models.Dtos.TaskOptionUpdateDto.RootObject updateTaskOption = TaskService.GetTaskOptions(StatusRecords);
                updateTaskOption.id = Task.Id;
                var taskResult = await DataService.UpdateTaskOptions(updateTaskOption);
                if (taskResult)
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("BAŞARILI", "İşlem Tamamlanmıştır.", "TAMAM");
                    await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("BAŞARISIZ", "İşlem Sırasında Hata Oluştu.", "TAMAM");
                }
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
    }
}