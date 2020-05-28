using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Source
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}