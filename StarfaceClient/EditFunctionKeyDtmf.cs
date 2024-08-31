using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyDtmf {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("dtmf")]
        public string Dtmf { get; set; }
    }
}
