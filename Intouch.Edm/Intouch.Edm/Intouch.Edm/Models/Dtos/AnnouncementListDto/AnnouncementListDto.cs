using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.AnnouncementListDto
{
    public class AnnouncementListDto
    {
    }

    public class Item
    {
        public bool isRead { get; set; }
        public DateTime createTime { get; set; }
        public DateTime readTime { get; set; }
        public int announcementId { get; set; }
        public string title { get; set; }
        public string text { get; set; }
        public int id { get; set; }
    }

    public class Result
    {
        public int unreadCount { get; set; }
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
