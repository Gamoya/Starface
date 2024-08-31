using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyQuickDial {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
    }
}
