using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyDnd {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
