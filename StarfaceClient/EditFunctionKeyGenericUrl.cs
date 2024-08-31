using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyGenericUrl {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("genericURL")]
        public string GenericUrl { get; set; }
    }
}
