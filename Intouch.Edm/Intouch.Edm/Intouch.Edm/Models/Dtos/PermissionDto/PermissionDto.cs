using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.PermissionDto
{
    public class PermissionDto
    {
    }

    public class Result
    {
        public List<string> permissions { get; set; }
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
