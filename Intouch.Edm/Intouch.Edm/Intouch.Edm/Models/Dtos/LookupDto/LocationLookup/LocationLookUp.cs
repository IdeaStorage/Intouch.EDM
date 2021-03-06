﻿using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.LookupDto.LocationLookup
{
    public class LocationLookUp
    {
    }

    public class Site
    {
        public string name { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public int id { get; set; }
    }

    public class Item
    {
        public Site site { get; set; }
        public bool nearest { get; set; }

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