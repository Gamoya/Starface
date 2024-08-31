using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Contact {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("accountId")]
        public long? AccountId { get; set; }
        [JsonPropertyName("jabberId")]
        public string JabberId { get; set; }
        [JsonPropertyName("blocks")]
        public List<BlockSchema> Blocks { get; set; }
        [JsonPropertyName("editable")]
        public bool Editable { get; set; }
        [JsonPropertyName("primaryExternalNumber")]
        public string PrimaryExternalNumber { get; set; }
        [JsonPropertyName("primaryInternalNumber")]
        public string PrimaryInternalNumber { get; set; }
        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }
    }
}
