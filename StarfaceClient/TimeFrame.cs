using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class TimeFrame {
        [JsonPropertyName("monday")]
        public bool Monday { get; set; }
        [JsonPropertyName("tuesday")]
        public bool Tuesday { get; set; }
        [JsonPropertyName("wednesday")]
        public bool Wednesday { get; set; }
        [JsonPropertyName("thursday")]
        public bool Thursday { get; set; }
        [JsonPropertyName("friday")]
        public bool Friday { get; set; }
        [JsonPropertyName("saturday")]
        public bool Saturday { get; set; }
        [JsonPropertyName("sunday")]
        public bool Sunday { get; set; }
        [JsonPropertyName("timeBegin")]
        public string TimeBegin { get; set; }
        [JsonPropertyName("timeEnd")]
        public string TimeEnd { get; set; }
    }
}
