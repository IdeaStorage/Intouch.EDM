namespace Intouch.Edm.Models.Dtos.CreateScenario
{
    public class CreateScnearioResult
    {
    }

    public class Result
    {
        public bool result { get; set; }
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
