using System.Collections.Generic;
using System.Text.Json.Serialization;

#if NET8_0_OR_GREATER

namespace Gamoya.Phone.Starface {
    [JsonSourceGenerationOptions()]
    [JsonSerializable(typeof(SignInNonceResult))]
    [JsonSerializable(typeof(SignInResult))]
    [JsonSerializable(typeof(ContactScheme))]
    [JsonSerializable(typeof(ContactList))]
    [JsonSerializable(typeof(ConferenceConfiguration))]
    [JsonSerializable(typeof(ConferenceConfigurationTexts))]
    [JsonSerializable(typeof(PhoneConfig))]
    [JsonSerializable(typeof(Group))]
    [JsonSerializable(typeof(PhoneNumberConfig))]
    [JsonSerializable(typeof(ManagedConference))]
    [JsonSerializable(typeof(Contact))]
    [JsonSerializable(typeof(VoiceMailbox))]
    [JsonSerializable(typeof(EditFunctionKey))]
    [JsonSerializable(typeof(List<User>))]
    [JsonSerializable(typeof(List<Account>))]
    [JsonSerializable(typeof(List<FmcPhone>))]
    [JsonSerializable(typeof(List<PhoneAssignment>))]
    [JsonSerializable(typeof(List<PhoneNumber>))]
    [JsonSerializable(typeof(List<Permission>))]
    [JsonSerializable(typeof(List<GroupListItem>))]
    [JsonSerializable(typeof(List<VoiceMailboxListItem>))]
    [JsonSerializable(typeof(List<CallService>))]
    [JsonSerializable(typeof(List<Redirection>))]
    [JsonSerializable(typeof(List<FunctionKeySet>))]
    [JsonSerializable(typeof(List<FunctionKey>))]
    [JsonSerializable(typeof(List<Tag>))]
    [JsonSerializable(typeof(List<PhoneNumberAssignment>))]
    [JsonSerializable(typeof(List<PhoneAssignmentNumber>))]
    [JsonSerializable(typeof(List<ManagedConferenceSummary>))]
    [JsonSerializable(typeof(List<long>))]
    [JsonSerializable(typeof(Error))]
    internal partial class SourceGenerationContext : JsonSerializerContext {
    }
}

#endif
