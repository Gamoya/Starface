using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ManagedConferenceSummary {
        [JsonPropertyName("conferenceId")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("isReadonly")]
        public bool IsReadonly { get; set; }
        [JsonPropertyName("isTerminated")]
        public bool IsTerminated { get; set; }
        [JsonPropertyName("startTime")]
        public long StartTime { get; set; }
    }
}
