using Newtonsoft.Json;

namespace MastodonBots.Models
{
    internal class StatusRequest
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
