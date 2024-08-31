using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyModuleActivationModuleSettings {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("moduleId")]
        public string ModuleId { get; set; }
        [JsonPropertyName("activated")]
        public bool Activated { get; set; }
    }
}
