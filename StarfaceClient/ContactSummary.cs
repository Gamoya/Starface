using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ContactSummary {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("summaryValues")]
        public List<string> SummaryValues { get; set; }
        [JsonPropertyName("phoneNumberValues")]
        public List<string> PhoneNumberValues { get; set; }
        [JsonPropertyName("additionalValues")]
        public Dictionary<string, string> AdditionalValues { get; set; }
    }
}
