using Intouch.Edm.Models;
using Intouch.Edm.Views;
using System;
using System.Collections.ObjectModel;
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
            TaskDetailLists = new ObservableCollection<TaskDetail>();

            StatusRecords = new ObservableCollection<HelperModel>();

            foreach (var item in taskItem.Options.result.items)
            {
                StatusRecords.Add(new HelperModel
                {
                    Name = item.checkedOption.text,
                    IsSelected = item.checkedOption.completed,
                    UserFullName = item.checkedOption.userFullName,
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
            string text = "05511813450";
            if (!string.IsNullOrEmpty(text))
            {
                var result = await CallConfirmSheet.ShowConfirmPopup(Application.Current.MainPage.Navigation, text);

                if (result)
                {
                    await Call(text);
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