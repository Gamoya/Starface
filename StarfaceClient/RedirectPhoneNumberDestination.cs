using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class RedirectPhoneNumberDestination {
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("redirectDestinationType")]
        public string RedirectDestinationType { get; set; }
    }
}
