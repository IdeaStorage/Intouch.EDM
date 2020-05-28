using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intouch.Edm.Models
{
    public class EventTypeDto
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<EventType> Items { get; set; }
    }
}