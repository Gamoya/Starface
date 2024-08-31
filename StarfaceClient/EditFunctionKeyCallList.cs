using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyCallList {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("callListRequest")]
        public string CallListRequest { get; set; }
        [JsonPropertyName("callListRequests")]
        public List<string> CallListRequests { get; set; }
    }
}
