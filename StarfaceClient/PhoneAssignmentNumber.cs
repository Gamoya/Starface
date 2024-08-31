using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneAssignmentNumber {
        [JsonPropertyName("phoneNumberId")]
        public long PhoneNumberId { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
