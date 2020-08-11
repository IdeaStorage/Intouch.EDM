using Intouch.Edm.Models;
using Intouch.Edm.Models.Dtos.TaskListDto;
using Intouch.Edm.ViewModels;
using System;
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
                Id = taskItem.userTask.id,
                ScenarioId = taskItem.scenarioId,
                profilePicture = new UriImageSource
                {
                    Uri = new Uri($"http://edm.intouch.istanbul/Profile/GetSmallProfilePictureByUserId?userId={taskItem.userTask.userId}"),
                    CacheValidity = TimeSpan.FromDays(1),
                    CachingEnabled = true
                }
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

        public static (string eventIcon, string eventName) GetEvent(int selectedEventId = 0)
        {
            switch (selectedEventId)
            {
                case (int)Events.Fire:
                    return ("fireDetail.png", "Yangın");

                case (int)Events.WaterFlood:
                    return ("waterDetail.png", "Su Baskını");

                case (int)Events.Earthquake:
                    return ("earthquakeDetail.png", "Deprem");

                case (int)Events.BusinessContuniuty:
                    return ("businessContinuityDetail.png", "İş Sürekliliği");

                case (int)Events.Pandemic:
                    return ("pandemicDetail.png", "Pandemi");

                case (int)Events.Other:
                    return ("othersDetail.png", "Diğer");

                default:
                    return ("", "");
            }
        }

        public static (string eventIcon, string eventName) GetEventByName(string eventName = "")
        {
            switch (eventName)
            {
                case "Yangın":
                    return ("fireListIcon.png", "Yangın");

                case "Su Baskını":
                    return ("waterListIcon.png", "Su Baskını");

                case "Deprem":
                    return ("earthquakeListIcon.png", "Deprem");

                case "İş Sürekliliği":
                    return ("businessListIcon.png", "İş Sürekliliği");

                case "Pandemi":
                    return ("pandemicListIcon.png", "Pandemi");

                case "Diğer":
                    return ("othersListIcon.png", "Diğer");

                default:
                    return ("", "");
            }
        }
    }
}