using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intouch.Edm.Models
{
    public class ImpactAreaDto
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<ImpactAreaItem> Items { get; set; }
    }
}