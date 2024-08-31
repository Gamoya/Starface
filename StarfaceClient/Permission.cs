using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Permission {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("permission")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
