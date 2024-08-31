using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyForwardNumberUnconditional {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("editFunctionKeyFnuNumberSetting")]
        public List<EditFunctionKeyForwardNumberUnconditionalNumberSetting> NumberSettings { get; set; }
    }
}
