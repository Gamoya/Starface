using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class ConferenceConfigurationTexts {
        [JsonPropertyName("eMailSubject")]
        public string EmailSubject { get; set; }
        [JsonPropertyName("eMailBody")]
        public string EmailBody { get; set; }
    }
}
