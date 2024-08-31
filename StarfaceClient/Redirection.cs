using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Redirection {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        [JsonPropertyName("groupNumber")]
        public bool GroupNumber { get; set; }
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonPropertyName("redirectDestination")]
        public RedirectDestination RedirectDestination { get; set; }
        [JsonPropertyName("redirectTrigger")]
        public RedirectTrigger RedirectTrigger { get; set; }
        [JsonPropertyName("lastMailboxDestination")]
        public RedirectMailboxDestination LastMailboxDestination { get; set; }
        [JsonPropertyName("lastPhoneNumberDestination")]
        public RedirectPhoneNumberDestination LastPhoneNumberDestination { get; set; }
    }
}
