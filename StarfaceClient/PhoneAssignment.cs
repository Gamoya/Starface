using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneAssignment {
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        [JsonPropertyName("phoneId")]
        public long PhoneId { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("isIFMC")]
        public bool IsIfmc { get; set; }
        [JsonPropertyName("phoneName")]
        public string PhoneName { get; set; }
        [JsonPropertyName("phoneType")]
        public string PhoneType { get; set; }
        [JsonPropertyName("starfaceType")]
        public string StarfaceType { get; set; }
    }
}
