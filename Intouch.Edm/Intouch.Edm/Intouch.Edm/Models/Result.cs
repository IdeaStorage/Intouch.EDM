using Newtonsoft.Json;

namespace Intouch.Edm.Models
{
    public class Result
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expireInSeconds")]
        public int ExpireInSeconds { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}