using System.Collections.Generic;

namespace Intouch.Edm.Models.Dtos.ScenarioTaskOptionsDto
{
    public class ScenarioTaskOptions
    {
    }
    public class Result
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool completed { get; set; }
        public int userId { get; set; }
        public string userFullName { get; set; }
    }

    public class Root
    {
        public List<Result> result { get; set; }
        public object targetUrl { get; set; }
        public bool success { get; set; }
        public object error { get; set; }
        public bool unAuthorizedRequest { get; set; }
        public bool __abp { get; set; }
    }


}
