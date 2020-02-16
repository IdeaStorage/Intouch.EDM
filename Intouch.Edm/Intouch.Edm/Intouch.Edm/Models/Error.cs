using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("details")]
        public string Details { get; set; }
        [JsonProperty("validationErrors")]
        public string ValidationErrors { get; set; }
    }
}
