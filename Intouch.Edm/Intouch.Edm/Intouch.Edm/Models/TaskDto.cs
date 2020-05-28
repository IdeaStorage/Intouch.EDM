using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intouch.Edm.Models
{
    public class TaskDto
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<TaskDtoItem> Items { get; set; }
    }
}