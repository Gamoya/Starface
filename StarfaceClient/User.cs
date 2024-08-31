using System;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class User {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("personId")]
        public Guid PersonId { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }
        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("missedCallReport")]
        public bool MissedCallReport { get; set; }
        [JsonPropertyName("faxCallerId")]
        public string FaxCallerId { get; set; }
        [JsonPropertyName("faxHeader")]
        public string FaxHeader { get; set; }
        [JsonPropertyName("faxCoverPage")]
        public bool FaxCoverPage{ get; set; }
        [JsonPropertyName("faxEmailJournal")]
        public bool FaxEmailJournal { get; set; }
        [JsonPropertyName("licenseType")]
        public string LicenseType { get; set; }
    }
}
