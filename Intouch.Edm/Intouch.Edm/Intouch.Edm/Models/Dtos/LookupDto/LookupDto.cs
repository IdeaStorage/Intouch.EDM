using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.LookupDto
{
    public class LookupDto
    {
    }

    public class Item
    {
        public int id { get; set; }
        public string displayName { get; set; }
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