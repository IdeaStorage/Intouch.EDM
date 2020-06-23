using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.TaskListDto;
using Intouch.Edm.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Intouch.Edm.Services
{
    public class TaskService
    {
        internal static TaskItem GetTask(Item taskItem)
        {
            TaskItem item = new TaskItem
            {
                Text = taskItem.userTask.text,
                AssignmentTime = taskItem.userTask.assignmentTime != null && taskItem.userTask.assignmentTime.HasValue ? taskItem.userTask.assignmentTime.Value.ToString("dd MMMM yyyy HH:mm") : "",
                ScenarioTaskId = taskItem.userTask.scenarioTaskId,
                UserName = taskItem.userName,
                Completed = taskItem.userTask.completed,
                StatusImage = taskItem.userTask.completed ? "approveIcon.png" : "waitingIcon.png",
                Id = taskItem.userTask.id,
                ScenarioId = taskItem.scenarioId,
                profilePicture = UriImageSource.FromUri(new System.Uri($"http://edm.intouch.istanbul/Profile/GetProfilePictureByUserId?userId={2}"))
            };
            return item;
        }

        internal static Models.Dtos.TaskOptionUpdateDto.RootObject GetTaskOptions(ObservableCollection<HelperModel> statusRecords)
        {
            Models.Dtos.TaskOptionUpdateDto.RootObject updateTaskOption = new Models.Dtos.TaskOptionUpdateDto.RootObject
            {
                checkedOptions = new List<Models.Dtos.TaskOptionUpdateDto.CheckedOption>()
            };
            foreach (var taskOption in statusRecords)
            {
                Models.Dtos.TaskOptionUpdateDto.CheckedOption updateOption = new Models.Dtos.TaskOptionUpdateDto.CheckedOption
                {
                    completed = taskOption.IsSelected,
                    id = taskOption.Id
                };
                updateTaskOption.checkedOptions.Add(updateOption);
            }

            return updateTaskOption;
        }
    }
}