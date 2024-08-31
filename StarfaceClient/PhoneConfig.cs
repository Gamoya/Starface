using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneConfig {
        [JsonPropertyName("callWaiting")]
        public bool CallWaiting { get; set; }
        [JsonPropertyName("displayNumberId")]
        public long DisplayNumberId { get; set; }
        [JsonPropertyName("doNotDisturb")]
        public bool DoNotDisturb { get; set; }
        [JsonPropertyName("primaryPhoneId")]
        public long PrimaryPhoneId { get; set; }
    }
}
