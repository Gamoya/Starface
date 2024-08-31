using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class VoiceMailbox {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("maximumDuration")]
        public long? MaximumDurationSeconds { get; set; }
        [JsonPropertyName("noRecord")]
        public bool? NoRecord { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("voicemailUsers")]
        public List<VoicemailUser> VoicemailUsers { get; set; }
        [JsonPropertyName("voicemailGroups")]
        public List<VoicemailGroup> VoicemailGroups { get; set; }
    }
}
