using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class AssignableUser {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("assigned")]
        public bool? Assigned { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
    }
}
