using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Location
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}