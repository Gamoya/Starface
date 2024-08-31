using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class BlockSchema {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("resourceKey")]
        public string ResourceKey { get; set; }
        [JsonPropertyName("attributes")]
        public List<BlockSchemaAttribute> Attributes { get; set; }
    }
}
