using System;

namespace Intouch.Edm.Models.Dtos.ViewScenario
{
    public class ViewScenarioDto
    {
    }

    public class Scenario
    {
        public int scenarioNo { get; set; }
        public int effectTime { get; set; }
        public bool admIsRequired { get; set; }
        public int admPersonCount { get; set; }
        public bool odmIsRequired { get; set; }
        public int odmPersonCount { get; set; }
        public string name { get; set; }
        public int subjectType { get; set; }
        public int siteId { get; set; }
        public Nullable<int> impactAreaId { get; set; }
        public int eventTypeId { get; set; }
        public object contactUserId { get; set; }
        public object contactFirmId { get; set; }
        public int sourceId { get; set; }
        public string id { get; set; }
        public string pictureUrl { get; set; }
        public string scenarioId { get; set; }
    }

    public class Result
    {
        public Scenario scenario { get; set; }
        public string siteName { get; set; }
        public string impactAreaName { get; set; }
        public string eventTypeName { get; set; }
        public object contactUserName { get; set; }
        public object contactFirmName { get; set; }
        public string sourceName { get; set; }
    }

    public class RootObject
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}