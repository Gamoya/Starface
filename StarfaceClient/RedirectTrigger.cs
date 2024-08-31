using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class RedirectTrigger {
        [JsonPropertyName("redirectTriggerType")]
        public string RedirectTriggerType { get; set; }
    }
}
