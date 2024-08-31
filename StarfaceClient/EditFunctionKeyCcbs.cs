using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyCcbs {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
