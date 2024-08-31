using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyModuleActivation {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("editFunctionKeyMaModuleSettings")]
        public List<EditFunctionKeyModuleActivationModuleSettings> ModuleSettings { get; set; }
    }
}
