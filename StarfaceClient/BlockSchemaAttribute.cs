using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class BlockSchemaAttribute {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("displayKey")]
        public string DisplayKey { get; set; }
        [JsonPropertyName("additionalValues")]
        public Dictionary<string, string> AdditionalValues { get; set; }
        [JsonPropertyName("i18nDisplayName")]
        public string DisplayName { get; set; }
    }
}
