using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models
{
    public class ScenarioDto
    {
        [JsonProperty("businessContinuityScenario")]
        public ScenarioItem ScenarioItem { get; set; }
        [JsonProperty("contactFirmName")]
        public string ContactFirmName { get; set; }
        [JsonProperty("contactUserName")]
        public string ContactUserName { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("siteName")]
        public string SiteName { get; set; }
        [JsonProperty("eventTypeName")]
        public string EventTypeName { get; set; }
        [JsonProperty("sourceName")]
        public string SourceName { get; set; }
        [JsonProperty("impactAreaName")]
        public string ImpactAreaName { get; set; }

    }
}
