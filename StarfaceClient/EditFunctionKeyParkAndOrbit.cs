using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKeyParkAndOrbit {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("parkAndOrbitNumber")]
        public string ParkAndOrbitNumber { get; set; }
        [JsonPropertyName("parkAndOrbitNumbers")]
        public List<string> ParkAndOrbitNumbers { get; set; }
    }
}
