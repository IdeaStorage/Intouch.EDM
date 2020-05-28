using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class TaskDtoItem
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("scenarioTaskText")]
        public string ScenarioTaskText { get; set; }

        [JsonProperty("userTask")]
        public TaskItem TaskItem { get; set; }
    }
}