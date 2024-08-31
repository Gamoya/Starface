using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class CallService {
        [JsonPropertyName("serviceId")]
        public long Id { get; set; }
        [JsonPropertyName("serviceName")]
        public string Name { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
