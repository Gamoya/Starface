using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class SignInNonceResult {
        [JsonPropertyName("loginType")]
        public string LoginType { get; set; }
        [JsonPropertyName("nonce")]
        public string Nonce { get; set; }
        [JsonPropertyName("secret")]
        public string Secret { get; set; }
    }
}
