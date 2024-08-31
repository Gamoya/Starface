using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeySeparator {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
