using System;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Tag {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
    }
}
