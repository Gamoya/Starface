using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneNumberAssignment {
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        [JsonPropertyName("phoneNumberId")]
        public long PhoneNumberId { get; set; }
        [JsonPropertyName("serviceId")]
        public long ServiceId { get; set; }
    }
}
