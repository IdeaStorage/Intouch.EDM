namespace Intouch.Edm.Models.Dtos.AnnouncementCountDto
{
    public class AnnouncementCountDto
    {
    }

    public class RootObject
    {
        public int result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}