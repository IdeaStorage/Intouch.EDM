using System;
using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.TaskListDto
{
    public class TaskListDto
    {
    }

    public class UserTask
    {
        public string text { get; set; }
        public bool completed { get; set; }
        public object completedTime { get; set; }
        public Nullable<DateTime> assignmentTime { get; set; }
        public bool isRead { get; set; }
        public object readTime { get; set; }
        public int userId { get; set; }
        public string scenarioTaskId { get; set; }
        public string id { get; set; }
    }

    public class Item
    {
        public UserTask userTask { get; set; }
        public string userName { get; set; }
        public string scenarioTaskText { get; set; }
        public string scenarioId { get; set; }
    }

    public class Result
    {
        public int totalCount { get; set; }
        public List<Item> items { get; set; }
    }

    public class RootObject
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}
