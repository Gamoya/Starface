using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ContactList {
        [JsonPropertyName("metadata")]
        public MetaData MetaData { get; set; }
        [JsonPropertyName("summaryBlockSchema")]
        public BlockSchema SummaryBlockSchema { get; set; }
        [JsonPropertyName("phoneNumbersBlockSchema")]
        public BlockSchema PhoneNumbersBlockSchema { get; set; }
        [JsonPropertyName("contacts")]
        public List<ContactSummary> Contacts { get; set; }
    }
}
