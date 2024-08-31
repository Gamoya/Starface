using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class FmcPhone {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("confirm")]
        public bool Confirm { get; set; }
        [JsonPropertyName("delay")]
        public long Delay { get; set; }
        [JsonPropertyName("fmcSchedule")]
        public List<TimeFrame> FmcSchedules { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("telephoneId")]
        public string TelephoneId { get; set; }
    }
}
