using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ContactScheme {
        [JsonPropertyName("detailBlocks")]
        public List<BlockSchema> DetailBlocks { get; set; }
        [JsonPropertyName("phoneNumbersBlock")]
        public BlockSchema PhoneNumbersBlock { get; set; }
        [JsonPropertyName("summaryBlock")]
        public BlockSchema SummaryBlock { get; set; }
    }
}
