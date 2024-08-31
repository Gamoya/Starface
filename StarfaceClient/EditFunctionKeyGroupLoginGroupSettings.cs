using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyGroupLoginGroupSettings {
        [JsonPropertyName("groupname")]
        public string GroupName { get; set; }
        [JsonPropertyName("groupId")]
        public long GroupId { get; set; }
        [JsonPropertyName("activated")]
        public bool Activated { get; set; }
    }
}
