using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class VoiceMailboxListItem {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("voiceboxname")]
        public string Name { get; set; }
        [JsonPropertyName("mailboxnumber")]
        public string Number { get; set; }
        [JsonPropertyName("assignedusers")]
        public List<string> AssignedUsers { get; set; }
        [JsonPropertyName("assignedgroups")]
        public List<string> AssignedGroups { get; set; }
    }
}
