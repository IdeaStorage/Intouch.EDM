using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.LookupDto.ImpactAreaLookup
{
    class ImpactAreaLookup
    {
    }

    public class ImpactArea
    {
        public string name { get; set; }
        public int siteId { get; set; }
        public int id { get; set; }
    }

    public class Item
    {
        public ImpactArea impactArea { get; set; }
        public string siteName { get; set; }
    }

    public class Result
    {
        public int totalCount { get; set; }
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
