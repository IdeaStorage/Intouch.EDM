using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
