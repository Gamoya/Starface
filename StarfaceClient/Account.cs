using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Account {
        [JsonPropertyName("accountId")]
        public long Id { get; set; }
        [JsonPropertyName("displayInformation")]
        public string DisplayInformation { get; set; }
        [JsonPropertyName("federationname")]
        public string FederationName { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("local")]
        public bool Local { get; set; }
        [JsonPropertyName("primaryInternalPhoneNumber")]
        public string PrimaryInternalPhoneNumber { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
