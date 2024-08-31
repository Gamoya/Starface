using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneNumberConfig {
        [JsonPropertyName("possibleSignalnumbers")]
        public List<PhoneNumber> PossibleSignalNumbers { get; set; }
        [JsonPropertyName("primaryExternalNumberId")]
        public long PrimaryExternalNumberId { get; set; }
        [JsonPropertyName("primaryInternalNumberId")]
        public long? PrimaryInternalNumberId { get; set; }
        [JsonPropertyName("signalingNumberId")]
        public long? SignalingNumberId { get; set; }
    }
}
