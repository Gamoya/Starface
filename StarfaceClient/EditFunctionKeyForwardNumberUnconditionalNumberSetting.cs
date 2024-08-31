using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyForwardNumberUnconditionalNumberSetting {
        [JsonPropertyName("numberId")]
        public long NumberId { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("group")]
        public bool Group { get; set; }
        [JsonPropertyName("activated")]
        public bool Activated { get; set; }
    }
}
