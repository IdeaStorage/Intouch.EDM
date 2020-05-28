using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class ScenarioItem
    {
        [JsonProperty("effectTime")]
        public int EffectTime { get; set; }

        [JsonProperty("admIsRequired")]
        public bool AdmIsRequired { get; set; }

        [JsonProperty("admPersonCount")]
        public int AdmPersonCount { get; set; }

        [JsonProperty("odmIsRequired")]
        public int OdmIsRequired { get; set; }

        [JsonProperty("odmPersonCount")]
        public int OdmPersonCount { get; set; }

        [JsonProperty("name")]
        public int Name { get; set; }

        [JsonProperty("subjectType")]
        public int SubjectType { get; set; }

        [JsonProperty("siteId")]
        public int SiteId { get; set; }

        [JsonProperty("impactAreaId")]
        public int ImpactAreaId { get; set; }

        [JsonProperty("eventTypeId")]
        public int EventTypeId { get; set; }

        [JsonProperty("contactUserId")]
        public int ContactUserId { get; set; }

        [JsonProperty("contactFirmId")]
        public int ContactFirmId { get; set; }

        [JsonProperty("sourceId")]
        public int SourceId { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}