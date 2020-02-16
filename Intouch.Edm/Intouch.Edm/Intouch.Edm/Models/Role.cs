using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intouch.Edm.Models
{
    public class Role
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
