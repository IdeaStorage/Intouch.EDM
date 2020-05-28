using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class ImpactAreaItem
    {
        [JsonProperty("siteName")]
        public string SiteName { get; set; }

        [JsonProperty("impactArea")]
        public ImpactArea ImpactArea { get; set; }
    }
}