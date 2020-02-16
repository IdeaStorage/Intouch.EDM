using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models
{
    public class EventType
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("subjecType")]
        public int SubjectType { get; set; }
    }
}
