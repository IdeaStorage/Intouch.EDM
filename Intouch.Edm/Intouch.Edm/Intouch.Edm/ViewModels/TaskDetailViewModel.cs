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

        //Binding ile ekrandan verilmeli. Düzeltme yapılacak.
        public string Description { get; set; }

        public bool IsImageVisible { get; set; }
        public bool IsPictureButtonVisible { get; set; }

        public TaskDetailViewModel(TaskItem taskItem = null)
        {
            Task = taskItem;
            TaskDetailLists = new ObservableCollection<TaskDetail>();

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
            IsPictureButtonVisible = !string.IsNullOrEmpty(taskItem.Scenario.PictureUrl);
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

        private ICommand _showPicture;

        public ICommand ShowPictureButton => _showPicture
         ?? (_showPicture = new Command(async () => await ShowPictureClicked()));

        private async System.Threading.Tasks.Task ShowPictureClicked()
        {
            if (string.IsNullOrEmpty(Task.Scenario.PictureUrl))
            {
                IsImageVisible = true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("UYARI", "Henüz Yüklenen Fotoğraf Bulunmuyor.", "TAMAM");
            }
        }

        private async System.Threading.Tasks.Task ExecuteSaveClicked()
        {
            Models.Dtos.TaskOptionUpdateDto.RootObject updateTaskOption = TaskService.GetTaskOptions(StatusRecords);
            updateTaskOption.id = Task.Id;
            updateTaskOption.description = Description ?? string.Empty;
            var taskResult = await DataService.UpdateTaskOptions(updateTaskOption);
            if (taskResult)
            {
                await Application.Current.MainPage.DisplayAlert("BAŞARILI", "İşlem Tamamlanmıştır.", "TAMAM");
                await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("BAŞARISIZ", "İşlem Sırasında Hata Oluştu.", "TAMAM");
            }
        }
    }
}