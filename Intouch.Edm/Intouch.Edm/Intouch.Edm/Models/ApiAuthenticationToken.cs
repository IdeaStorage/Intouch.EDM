using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class ApiAuthenticationToken
    {
        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}