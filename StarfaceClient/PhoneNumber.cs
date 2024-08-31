using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class PhoneNumber {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("assignedGroupAccountId")]
        public long? AssignedGroupAccountId { get; set; }
        [JsonPropertyName("assignedModuleInstanceId")]
        public string AssignedModuleInstanceId { get; set; }
        [JsonPropertyName("assignedServiceId")]
        public long? AssignedServiceId { get; set; }
        [JsonPropertyName("assignedUserAccountId")]
        public long? AssignedUserAccountId { get; set; }
        [JsonPropertyName("exitCode")]
        public string ExitCode { get; set; }
        [JsonPropertyName("localPrefix")]
        public string LocalPrefix { get; set; }
        [JsonPropertyName("nationalPrefix")]
        public string NationalPrefix { get; set; }
        [JsonPropertyName("number")]
        public string Number { get; set; }
        [JsonPropertyName("numberBlockId")]
        public long? NumberBlockId { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }


    }
}
