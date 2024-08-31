using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class FunctionKey {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }
        [JsonPropertyName("activeModuleIds")]
        public List<string> ActiveModuleIds { get; set; }
        [JsonPropertyName("addressBookFolderName")]
        public string AddressBookFolderName { get; set; }
        [JsonPropertyName("addressbookRequest")]
        public string AddressBookRequest { get; set; }
        [JsonPropertyName("blfAccountId")]
        public long? BlfAccountId { get; set; }
        [JsonPropertyName("callListRequest")]
        public string CallListRequest { get; set; }
        [JsonPropertyName("directCallTargetnumber")]
        public string DirectCallTargetNumber { get; set; }
        [JsonPropertyName("displayNumberId")]
        public long? DisplayNumberId { get; set; }
        [JsonPropertyName("dtmf")]
        public string Dtmf { get; set; }
        [JsonPropertyName("forwardType")]
        public string ForwardType { get; set; }
        [JsonPropertyName("functionKeyType")]
        public string FunctionKeyType { get; set; }
        [JsonPropertyName("genericURL")]
        public string GenericUrl { get; set; }
        [JsonPropertyName("groupIds")]
        public List<long> GroupIds { get; set; }
        [JsonPropertyName("poNumber")]
        public string PoNumber { get; set; }
        [JsonPropertyName("position")]
        public long Position { get; set; }
        [JsonPropertyName("redirectNumberIds")]
        public List<long> RedirectNumberIds { get; set; }
    }
}
