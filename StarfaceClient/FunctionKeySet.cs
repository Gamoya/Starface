using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class FunctionKeySet {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("keyOrder")]
        public List<string> KeyOrder { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
