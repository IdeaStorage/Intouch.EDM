using Newtonsoft.Json;
using System;
using Xamarin.Forms;

namespace Intouch.Edm.Models
{
    public class TaskItem
    {
        [JsonProperty("id")]
        public String Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("completedTime")]
        public DateTime CompletedTime { get; set; }

        [JsonProperty("assignmentTime")]
        public string AssignmentTime { get; set; }

        [JsonProperty("readTime")]
        public DateTime ReadTime { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("scenarioTaskId")]
        public string ScenarioTaskId { get; set; }

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }

        public string UserName { get; set; }
        public string ScenarioTaskText { get; set; }
        public Dtos.TaskOptionDto.RootObject Options { get; set; }

        public string ScenarioId { get; set; }
        public Dtos.ViewScenario.RootObject ScenarioDto { get; set; }
        public Scenario Scenario { get; set; }

        public UriImageSource profilePicture { get; set; }
    }
}