using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class RedirectDestination {
        [JsonPropertyName("redirectDestinationType")]
        public string RedirectDestinationType { get; set; }
    }
}
