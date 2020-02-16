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
        public string StatusIcon { get; set; }
        public string Statu { get; set; }
        public string Icon { get; set; }
        public string RecordDate { get; set; }
        public string PictureUrl { get; set; }
        public string ScenarioId { get; set; }

        public bool IsWaiting { get; set; }
    }
}

/*
 {"result":{"totalCount":3,"items":[
 {"emergencyScenario":{"effectTime":0,"admIsRequired":false,"admPersonCount":0,"odmIsRequired":false,
 "odmPersonCount":0,"name":"","subjectType":1,"siteId":1,"impactAreaId":2,"eventTypeId":2,"contactUserId":null,
 "contactFirmId":null,"sourceId":4,"id":"7dbe821e-4bc8-4254-fc67-08d787ab5da5"},"siteName":"Bileşim Genel Müdürlük","impactAreaName":"B1 Katı","eventTypeName":"Yangın","contactUserName":"","contactFirmName":"","sourceName":"Elektrik Kontağı"},{"emergencyScenario":{"effectTime":0,"admIsRequired":false,"admPersonCount":0,"odmIsRequired":false,"odmPersonCount":0,"name":"","subjectType":1,"siteId":1,"impactAreaId":1,"eventTypeId":2,"contactUserId":null,"contactFirmId":null,"sourceId":4,"id":"7e95737e-eed9-4199-fc66-08d787ab5da5"},"siteName":"Bileşim Genel Müdürlük","impactAreaName":"B2 Katı","eventTypeName":"Yangın","contactUserName":"","contactFirmName":"","sourceName":"Elektrik Kontağı"},{"emergencyScenario":{"effectTime":0,"admIsRequired":false,"admPersonCount":0,"odmIsRequired":false,"odmPersonCount":0,"name":"","subjectType":1,"siteId":1,"impactAreaId":null,"eventTypeId":3,"contactUserId":null,"contactFirmId":null,"sourceId":7,"id":"a992b7d6-da85-4fe4-1476-08d7879bf79d"},"siteName":"Bileşim Genel Müdürlük","impactAreaName":"","eventTypeName":"Deprem","contactUserName":"","contactFirmName":"","sourceName":"Afet"}]},"targetUrl":null,"success":true,"error":null,"unAuthorizedRequest":false,"__abp":true}
     
     */
