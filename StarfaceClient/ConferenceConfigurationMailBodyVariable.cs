using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ConferenceConfigurationMailBodyVariable {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("placeholder")]
        public string Placeholder { get; set; }
    }
}
