using System.Text.Json.Serialization;

namespace MastodonBots.Models
{
    internal class StatusRequest
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
