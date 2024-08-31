using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyPhoneContact {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("addressbookRequest")]
        public string AddressBookRequest { get; set; }
        [JsonPropertyName("addressbookRequests")]
        public List<string> AddressBookRequests { get; set; }
        [JsonPropertyName("folders")]
        public List<string> Folders { get; set; }
        [JsonPropertyName("selectedFolder")]
        public string SelectedFolder { get; set; }
    }
}
