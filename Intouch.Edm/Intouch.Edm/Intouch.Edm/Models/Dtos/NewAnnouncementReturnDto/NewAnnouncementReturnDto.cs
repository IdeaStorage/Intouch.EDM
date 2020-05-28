using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.NewAnnouncementReturnDto
{
    internal class NewAnnouncementReturnDto
    {
    }

    public class ValidationError
    {
        public string message { get; set; }
        public List<string> members { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public string details { get; set; }
        public List<ValidationError> validationErrors { get; set; }
    }

    public class RootObject
    {
        public object result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public Error error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }
}