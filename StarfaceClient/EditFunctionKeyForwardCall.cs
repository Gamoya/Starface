using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyForwardCall {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("forwardType")]
        public string ForwardType { get; set; }
        [JsonPropertyName("forwardTypes")]
        public List<string> ForwardTypes { get; set; }
    }
}
