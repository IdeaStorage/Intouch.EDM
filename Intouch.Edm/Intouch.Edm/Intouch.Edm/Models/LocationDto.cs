using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intouch.Edm.Models
{
    public class LocationDto
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
        [JsonProperty("items")]
        public List<Location> Items { get; set; }
    }
}
