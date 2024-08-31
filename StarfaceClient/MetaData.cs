using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class MetaData {
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("pagesize")]
        public int PageSize { get; set; }
        [JsonPropertyName("sort")]
        public string Sort { get; set; }
        [JsonPropertyName("sortdirection")]
        public string SortDirection { get; set; }
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }
}
