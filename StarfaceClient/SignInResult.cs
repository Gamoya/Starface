using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class SignInResult {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
