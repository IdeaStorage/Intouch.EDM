using Intouch.Edm.Models;
using Intouch.Edm.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Intouch.Edm.ViewModels
{
    public class UserTaskOptionsViewModel : BaseViewModel
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

        private string _userFullName;

        public string UserPhone { get; set; }

        public string UserFullName
        {
            get { return _userFullName; }
            set
            {
                _userFullName = value;
                OnPropertyChanged();
            }
        }

        private int _totalTaskCount;

        public int TotalTaskCount
        {
            get { return _totalTaskCount; }
            set
            {
                _totalTaskCount = value;
                OnPropertyChanged();
            }
        }

        private int _completedTaskCount;

        public int CompletedTaskCount
        {
            get { return _completedTaskCount; }
            set
            {
                _completedTaskCount = value;
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

        public UserTaskOptionsViewModel(TaskItem taskItem)
        {
            Task = taskItem;
            UserFullName = taskItem.Options.result.items.FirstOrDefault().checkedOption.userFullName;
            UserPhone = taskItem.Options.result.items.FirstOrDefault().checkedOption.taskUserPhone;
            TotalTaskCount = taskItem.Options.result.items.Count;
            CompletedTaskCount = taskItem.Options.result.items.Count(a => a.checkedOption.completed);
            TaskDetailLists = new ObservableCollection<TaskDetail>();

            StatusRecords = new ObservableCollection<HelperModel>();

            foreach (var item in taskItem.Options.result.items)
            {
                StatusRecords.Add(new HelperModel
                {
                    Name = item.checkedOption.text,
                    IsSelected = item.checkedOption.completed,
                    UserFullName = item.checkedOption.userFullName,
                    UserPhone = item.checkedOption.taskUserPhone,
                    Id = item.checkedOption.id,
                    UserId = item.checkedOption.userId
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

        private ICommand _callCommand;

        public ICommand CallCommand => _callCommand
                ?? (_callCommand = new Command(async () => await ExecuteCallCommand()));

        private async System.Threading.Tasks.Task ExecuteCallCommand()
        {
            if (!string.IsNullOrEmpty(UserPhone))
            {
                var result = await CallConfirmSheet.ShowConfirmPopup(Application.Current.MainPage.Navigation, UserPhone);

                if (result)
                {
                    await Call(UserPhone);
                }
            }
        }

        public async Task Call(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}