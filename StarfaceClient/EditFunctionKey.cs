using System.Text.Json.Serialization;

namespace Gamoya.Phone.Starface {
    public class EditFunctionKey {
        [JsonPropertyName("editFunctionKeyBusyLampField")]
        public EditFunctionKeyBusyLampField BusyLampField { get; set; }
        [JsonPropertyName("editFunctionKeyCallList")]
        public EditFunctionKeyCallList CallList { get; set; }
        [JsonPropertyName("editFunctionKeyCcbs")]
        public EditFunctionKeyCcbs Ccbs { get; set; }
        [JsonPropertyName("editFunctionKeyDnd")]
        public EditFunctionKeyDnd Dnd { get; set; }
        [JsonPropertyName("editFunctionKeyDtmf")]
        public EditFunctionKeyDtmf Dtmf { get; set; }
        [JsonPropertyName("editFunctionKeyForwardCall")]
        public EditFunctionKeyForwardCall ForwardCall { get; set; }
        [JsonPropertyName("editFunctionKeyForwardNumberUnconditional")]
        public EditFunctionKeyForwardNumberUnconditional ForwardNumberUnconditional { get; set; }
        [JsonPropertyName("editFunctionKeyGenericUrl")]
        public EditFunctionKeyForwardNumberUnconditional GenericUrl { get; set; }
        [JsonPropertyName("editFunctionKeyGroupLogin")]
        public EditFunctionKeyGroupLogin GroupLogin { get; set; }
        [JsonPropertyName("editFunctionKeyModuleActivation")]
        public EditFunctionKeyModuleActivation ModuleActivation { get; set; }
        [JsonPropertyName("editFunctionKeyParkAndOrbit")]
        public EditFunctionKeyParkAndOrbit ParkAndOrbit { get; set; }
        [JsonPropertyName("editFunctionKeyPhoneContact")]
        public EditFunctionKeyPhoneContact PhoneContact { get; set; }
        [JsonPropertyName("editFunctionKeyQuickDial")]
        public EditFunctionKeyQuickDial QuickDial { get; set; }
        [JsonPropertyName("editFunctionKeySeperator")]
        public EditFunctionKeySeparator Separator { get; set; }
        [JsonPropertyName("editFunctionKeySignalNumber")]
        public EditFunctionKeySignalNumber SignalNumber { get; set; }
    }
}
