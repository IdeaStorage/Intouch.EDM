using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.Scenario
{
    public class Scenario
    {

    }
    public class Item
    {
        public object commiteeApprovalId { get; set; }
        public string name { get; set; }
        public string parent { get; set; }
        public bool hasSubordinates { get; set; }
        public string siteName { get; set; }
        public string impactAreaName { get; set; }
        public string eventTypeName { get; set; }
        public string contactUserName { get; set; }
        public object contactFirmName { get; set; }
        public string sourceName { get; set; }
        public int type { get; set; }
        public int subjectType { get; set; }
        public DateTime creationTime { get; set; }
        public string scenarioId { get; set; }
        public object scenarioTaskId { get; set; }
        public string pictureUrl { get; set; }
        public int? approveStatus { get; set; }
        public string id { get; set; }
    }

    public class Result
    {
        public List<Item> items { get; set; }
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
