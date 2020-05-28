using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intouch.Edm.Models
{
    public class SourceDto
    {
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("items")]
        public List<Source> Items { get; set; }
    }
}