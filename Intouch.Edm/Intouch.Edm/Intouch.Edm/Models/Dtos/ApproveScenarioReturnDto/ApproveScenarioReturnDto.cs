using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.ApproveScenarioReturnDto
{
    public class ApproveScenarioReturnDto
    {
    }

    public class Result
    {
        public bool approveResult { get; set; }
        public string message { get; set; }
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
