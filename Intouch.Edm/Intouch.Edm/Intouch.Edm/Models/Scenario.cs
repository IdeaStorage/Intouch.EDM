using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Scenario
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public int SubjectId { get; set; }
        public int SiteId { get; set; }
        public int EventId { get; set; }
        public int SourceId { get; set; }
        public int ImpactAreaId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Subject { get; set; }
        public string Site { get; set; }
        public string Event { get; set; }
        public string Source { get; set; }
        public string ImpactArea { get; set; }
        public string Description { get; set; }
        public string Statu { get; set; }
        public string Icon { get; set; }
        public string RecordDate { get; set; }
        public string PictureUrl { get; set; }
        public string ScenarioId { get; set; }

        public bool IsWaiting { get; set; }
        public string commiteeApprovalId { get; set; }
    }
}