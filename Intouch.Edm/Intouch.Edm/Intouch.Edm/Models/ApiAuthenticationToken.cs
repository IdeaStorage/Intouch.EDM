using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
