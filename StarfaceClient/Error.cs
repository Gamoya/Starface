using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class Error {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
