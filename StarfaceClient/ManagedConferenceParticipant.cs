using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ManagedConferenceParticipant {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("eMailAddress")]
        public string EmailAddress { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("isModerator")]
        public bool IsModerator { get; set; }
        [JsonPropertyName("callOnStart")]
        public bool CallOnStart { get; set; }
        [JsonPropertyName("participantId")]
        public long? ParticipantId { get; set; }
    }
}
