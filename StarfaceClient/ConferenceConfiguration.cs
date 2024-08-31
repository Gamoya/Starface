using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ConferenceConfiguration {
        [JsonPropertyName("eMailSubject")]
        public string EmailSubject { get; set; }
        [JsonPropertyName("eMailBody")]
        public string EmailBody { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }
        [JsonPropertyName("externalNumberId")]
        public long? ExternalNumberId { get; set; }
        [JsonPropertyName("internalNumberId")]
        public long? InternalNumberId { get; set; }
        [JsonPropertyName("serverAddress")]
        public string ServerAddress { get; set; }
        [JsonPropertyName("variables")]
        public List<ConferenceConfigurationMailBodyVariable> Variables { get; set; }
    }
}
