using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyBusyLampField {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("blfAccountId")]
        public long BlfAccountId { get; set; }
        [JsonPropertyName("blfDisplayInformation")]
        public string BlfDisplayInformation { get; set; }
        [JsonPropertyName("availableAccounts")]
        public List<Account> AvailableAccounts { get; set; }
    }
}
