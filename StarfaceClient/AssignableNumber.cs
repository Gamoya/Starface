using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class AssignableNumber {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("intern")]
        public bool? Intern { get; set; }
        [JsonPropertyName("localAreaCode")]
        public string LocalAreaCode { get; set; }
        [JsonPropertyName("assigned")]
        public bool? Assigned { get; set; }
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
        [JsonPropertyName("exitCode")]
        public string ExitCode { get; set; }
        [JsonPropertyName("extension")]
        public string Extension { get; set; }
    }
}
