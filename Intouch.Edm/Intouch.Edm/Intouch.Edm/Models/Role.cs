using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Role
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}