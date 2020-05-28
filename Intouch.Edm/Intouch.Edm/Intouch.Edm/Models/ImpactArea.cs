using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class ImpactArea
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("siteId")]
        public int SiteId { get; set; }
    }
}