using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models.Dtos.NewScenarioResultDto
{
    public class NewScenarioResult
    {
    }
    public class Result
    {
        public bool result { get; set; }
        public object scenarioId { get; set; }
        public string message { get; set; }
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
