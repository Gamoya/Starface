using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeySignalNumber {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("phoneNumberId")]
        public long PhoneNumberId { get; set; }
        [JsonPropertyName("possibleSignalnumbers")]
        public List<PhoneNumber> PossibleSignalNumbers { get; set; }
    }
}
