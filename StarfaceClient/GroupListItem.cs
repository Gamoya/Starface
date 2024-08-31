using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class GroupListItem {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("groupname")]
        public string Name { get; set; }
        [JsonPropertyName("groupinternalnumber")]
        public string InternalNumber { get; set; }
        [JsonPropertyName("groupexternalnumber")]
        public string ExternalNumber { get; set; }
    }
}
