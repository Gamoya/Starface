using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Group {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("voicemail")]
        public bool? Voicemail { get; set; }
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; }
        [JsonPropertyName("chatgroup")]
        public bool? ChatGroup { get; set; }
        [JsonPropertyName("assignableNumbers")]
        public List<AssignableNumber> AssignableNumbers { get; set; }
        [JsonPropertyName("assignableUsers")]
        public List<AssignableUser> AssignableUsers { get; set; }
    }
}
