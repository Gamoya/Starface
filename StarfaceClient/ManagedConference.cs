using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ManagedConference {
        [JsonPropertyName("conferenceId")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("eMailBody")]
        public string EmailBody { get; set; }
        [JsonPropertyName("occurance")]
        public string Occurance { get; set; }
        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }
        [JsonPropertyName("participants")]
        public List<ManagedConferenceParticipant> Participants { get; set; }
    }
}
