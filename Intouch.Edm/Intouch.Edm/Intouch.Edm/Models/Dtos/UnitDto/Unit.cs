using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.UnitDto
{
    public class Unit
    {
        public string name { get; set; }
        public int id { get; set; }
    }

    public class Item
    {
        public Unit unit { get; set; }
    }

    public class Result
    {
        public int totalCount { get; set; }
        public List<Item> items { get; set; }
    }

    public class Root
    {
        public Result result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}
