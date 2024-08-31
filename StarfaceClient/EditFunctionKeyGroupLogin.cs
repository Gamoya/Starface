using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyGroupLogin {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("editFunctionKeyGlGroupSettings")]
        public List<EditFunctionKeyGroupLoginGroupSettings> GroupSettings { get; set; }
    }
}
