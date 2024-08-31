using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class VoicemailUser {
        [JsonPropertyName("accountId")]
        public long? AccountId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("sendMail")]
        public bool? SendMail { get; set; }
        [JsonPropertyName("voicemailboxName")]
        public string VoiceMailboxName { get; set; }
    }
}
