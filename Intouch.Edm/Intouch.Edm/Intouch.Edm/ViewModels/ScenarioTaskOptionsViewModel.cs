using Intouch.Edm.Models;
using System.Collections.ObjectModel;

namespace Intouch.Edm.ViewModels
{
    public class ScenarioTaskOptionsViewModel : BaseViewModel
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

        public ScenarioTaskOptionsViewModel(TaskItem taskItem)
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

    }
}
