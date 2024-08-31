using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class RedirectMailboxDestination {
        [JsonPropertyName("mailboxId")]
        public string MailboxId { get; set; }
        [JsonPropertyName("redirectDestinationType")]
        public string RedirectDestinationType { get; set; }
    }
}
