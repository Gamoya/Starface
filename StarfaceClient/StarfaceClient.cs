using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gamoya.Phone.Starface {
    public class StarfaceClient : IDisposable {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions() {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;
        private readonly bool _disposeHttpClient;

        private string AuthToken { get; set; }

#if NET8_0_OR_GREATER

        static StarfaceClient() {
            _jsonSerializerOptions.TypeInfoResolver = SourceGenerationContext.Default;
        }

#endif

        public StarfaceClient(string apiUrl) {
            _httpClient = CreateHttpClient(apiUrl);
            _disposeHttpClient = true;
        }

        public StarfaceClient(HttpClient httpClient) {
            _httpClient = httpClient;
            _disposeHttpClient = false;
        }

        public static HttpClient CreateHttpClient(string apiUrl) {
            if (!apiUrl.EndsWith("/")) {
                apiUrl += "/";
            }
            var httpClient = new HttpClient {
                BaseAddress = new Uri(apiUrl)
            };

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-Version", "2");

            return httpClient;
        }

        public void Dispose() {
            if (_disposeHttpClient) {
                _httpClient.Dispose();
            }
        }

        private static async Task<T> ResultAsync<T>(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) {
                return JsonSerializer.Deserialize<T>(content, _jsonSerializerOptions);
            } else {
                Error error;
                try {
                    error = JsonSerializer.Deserialize<Error>(content, _jsonSerializerOptions);
                } catch {
                    throw new StarfaceException(content);
                }
                throw new StarfaceException(error.Message);
            }
        }

        private static async Task CheckErrorAsync(HttpResponseMessage response) {
            if (response.IsSuccessStatusCode) {
                return;
            } else {
                var content = await response.Content.ReadAsStringAsync();
                Error error;
                try {
                    error = JsonSerializer.Deserialize<Error>(content, _jsonSerializerOptions);
                } catch {
                    throw new StarfaceException(content);
                }
                throw new StarfaceException(error.Message);
            }
        }

        private static string EscapeParameter(string value) {
            if (value == null) {
                return null;
            } else {
                return Uri.EscapeDataString(value);
            }
        }

        private static string GetSortDirection(ListSortDirection sortDirection) {
            if (sortDirection == ListSortDirection.Descending) {
                return "DESC";
            } else {
                return "ASC";
            }
        }

        #region SignIn/SignOut

        private async Task<SignInNonceResult> GetSignInNonceAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/login")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<SignInNonceResult>(response);
                }
            }
        }

        public async Task<bool> SignInAsync(string username, string password) {
            var nonceResult = await GetSignInNonceAsync();
            var hashAlgorithm = System.Security.Cryptography.SHA512.Create();
            var passwordHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password));
            var passwordHashString = string.Concat(passwordHash.Select(b => b.ToString("x2")));
            var secretHash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", username, nonceResult.Nonce, passwordHashString)));
            var secretHashString = string.Concat(secretHash.Select(b => b.ToString("x2")));

            nonceResult.Secret = string.Format("{0}:{1}", username, secretHashString);

            using (var message = new HttpRequestMessage(HttpMethod.Post, "rest/login")) {
                message.Content = new StringContent(JsonSerializer.Serialize(nonceResult, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    var result = await ResultAsync<SignInResult>(response);
                    AuthToken = result.Token;
                    if (!string.IsNullOrWhiteSpace(AuthToken)) {
                        _httpClient.DefaultRequestHeaders.Remove("authToken");
                        _httpClient.DefaultRequestHeaders.Add("authToken", AuthToken);
                    }
                    return !string.IsNullOrWhiteSpace(AuthToken);
                }
            }
        }

        public async Task SignOutAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, "rest/login")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    if (response.IsSuccessStatusCode) {
                        AuthToken = null;
                        _httpClient.DefaultRequestHeaders.Remove("authToken");
                    }
                }
            }
        }

        #endregion

        #region Users

        public async Task CreateUserAsync(User user) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, "rest/users")) {
                message.Content = new StringContent(JsonSerializer.Serialize(user, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<User>> GetUsersAsync(string searchTerm) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users?searchTerm={0}", EscapeParameter(searchTerm)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<User>>(response);
                }
            }
        }

        public async Task<List<User>> GetUsersWithPermissionAsync(long permissionId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/permissions/{0}/users", permissionId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<User>>(response);
                }
            }
        }

        public async Task<User> GetMeAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/users/me")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<User>(response);
                }
            }
        }

        public async Task<User> GetUserAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<User>(response);
                }
            }
        }

        public async Task UpdateUserAsync(User user) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}", user.Id))) {
                message.Content = new StringContent(JsonSerializer.Serialize(user, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteUserAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/users/{0}", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region Avatar

        public async Task<DocumentData> GetUserAvatarAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/avatar", userId))) {
                message.Headers.Add("Accept", "*/*");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return null;
                    }
                    var documentData = new DocumentData {
                        ContentType = response.Content.Headers.ContentType.MediaType,
                        Data = await response.Content.ReadAsByteArrayAsync()
                    };
                    return documentData;
                }
            }
        }

        public async Task UpdateUserAvatarAsync(long userId, DocumentData avatar) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/avatar", userId))) {
                message.Content = new ByteArrayContent(avatar.Data);
                message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(avatar.ContentType);
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteUserAvatarAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/users/{0}/avatar", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region PhoneConfig

        public async Task<PhoneConfig> GetPhoneConfigAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phoneconfig", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneConfig>(response);
                }
            }
        }

        public async Task UpdatePhoneConfigAsync(long userId, PhoneConfig phoneConfig) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/phoneconfig", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneConfig, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task CreatePhoneAssignmentAsync(long userId, PhoneAssignment phoneAssignment) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/users/{0}/phoneconfig/phones", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneAssignment, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<PhoneAssignment>> GetPhoneAssignmentsAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phoneconfig/phones", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneAssignment>>(response);
                }
            }
        }

        public async Task<PhoneAssignment> GetPhoneAssignmentAsync(long userId, long phoneId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phoneconfig/phones/{1}", userId, phoneId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneAssignment>(response);
                }
            }
        }

        public async Task DeletePhoneAssignmentAsync(long userId, long phoneId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/users/{0}/phoneconfig/phones/{1}", userId, phoneId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<PhoneAssignmentNumber>> GetPhoneAssignmentNumbersAsync(long userId, long phoneId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phoneconfig/phones/{1}/numbers", userId, phoneId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneAssignmentNumber>>(response);
                }
            }
        }

        public async Task UpdatePhoneAssignmentNumbersAsync(long userId, long phoneId, List<PhoneAssignmentNumber> phoneAssignmentNumbers) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/phoneconfig/phones/{1}/numbers", userId, phoneId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneAssignmentNumbers, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region PhoneNumberConfig

        public async Task<PhoneNumberConfig> GetPhoneNumberConfigAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phonenumberconfig", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneNumberConfig>(response);
                }
            }
        }

        public async Task<PhoneNumberConfig> UpdatePhoneNumberConfigAsync(long userId, PhoneNumberConfig phoneNumberConfig) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/phonenumberconfig", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneNumberConfig, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneNumberConfig>(response);
                }
            }
        }

        public async Task<List<PhoneNumberAssignment>> CreatePhoneNumberAssignmentsAsync(long userId, List<PhoneNumberAssignment> phoneNumberAssignments) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneNumberAssignments, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneNumberAssignment>>(response);
                }
            }
        }

        public async Task<List<PhoneNumberAssignment>> GetPhoneNumberAssignmentsAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneNumberAssignment>>(response);
                }
            }
        }

        public async Task<PhoneNumberAssignment> GetPhoneNumberAssignmentAsync(long userId, long phoneNumberId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers/{1}", userId, phoneNumberId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneNumberAssignment>(response);
                }
            }
        }

        public async Task<List<PhoneNumberAssignment>> UpdatePhoneNumberAssignmentsAsync(long userId, List<PhoneNumberAssignment> phoneNumberAssignments) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneNumberAssignments, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneNumberAssignment>>(response);
                }
            }
        }

        public async Task DeletePhoneNumberAssignmentAsync(long userId, long phoneNumberId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers/{1}", userId, phoneNumberId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeletePhoneNumberAssignmentsAsync(long userId, List<PhoneNumberAssignment> phoneNumberAssignments) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/users/{0}/phonenumberconfig/phonenumbers/delete", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(phoneNumberAssignments, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region ManagedConference

        public async Task CreateManagedConferenceAsync(long userId, ManagedConference managedConference) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/users/{0}/managedConferences", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(managedConference, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<ManagedConferenceSummary>> GetManagedConferencesAsync(long userId, int pageIndex, int? pageSize, string sortField, ListSortDirection sortDirection) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/managedConferences?page={1}&pagesize={2}&sort={3}&sortdirection={4}", userId, pageIndex, pageSize, EscapeParameter(sortField), GetSortDirection(sortDirection)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<ManagedConferenceSummary>>(response);
                }
            }
        }

        public async Task<ManagedConference> GetManagedConferenceAsync(long userId, long conferenceId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/users/{0}/managedConferences/{1}", userId, conferenceId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ManagedConference>(response);
                }
            }
        }

        public async Task StartManagedConferenceAsync(long userId, long conferenceId) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/managedConferences/{1}/start", userId, conferenceId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<ManagedConference> UpdateManagedConferenceAsync(long userId, ManagedConference managedConference) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/users/{0}/managedConferences/{1}", userId, managedConference.Id))) {
                message.Content = new StringContent(JsonSerializer.Serialize(managedConference, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ManagedConference>(response);
                }
            }
        }

        public async Task DeleteManagedConferenceAsync(long userId, long conferenceId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/users/{0}/managedConferences/{1}", userId, conferenceId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region Accounts

        public async Task<List<Account>> GetAccountsAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/accounts")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<Account>>(response);
                }
            }
        }

        #endregion

        #region Contacts

        public async Task<ContactScheme> GetContactSchemeAsync(string language) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts/scheme?lang={0}", language))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ContactScheme>(response);
                }
            }
        }

        public async Task<Contact> CreateContactAsync(Contact contact, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/contacts?userId={0}", actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(contact, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<Contact>(response);
                }
            }
        }

        public async Task<ContactList> GetContactsAsync(Guid[] tagIds, string searchTerms, int pageIndex, int? pageSize, string sortField, ListSortDirection sortDirection, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts?searchTerms={0}{1}&page={2}&pagesize={3}&sort={4}&sortdirection={5}&userId={6}", EscapeParameter(searchTerms), tagIds != null && tagIds.Length > 0 ? string.Format("&tags={0}", EscapeParameter(string.Join(",", tagIds))) : null, pageIndex, pageSize, sortField, GetSortDirection(sortDirection), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ContactList>(response);
                }
            }
        }

        public async Task<Contact> GetContactAsync(Guid contactId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts/{0}?userId={1}", contactId, actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<Contact>(response);
                }
            }
        }

        public async Task UpdateContactAsync(Contact contact, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts/{0}?userId={1}", contact.Id, actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(contact, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteContactAsync(Guid contactId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/contacts/{0}", contactId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region Tags

        public async Task CreateTagAsync(Tag tag, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/contacts/tags?userId={0}", actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(tag, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<Tag>> GetTagsAsync(int pageIndex, int? pageSize, string sortField, ListSortDirection sortDirection, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts/tags?page={0}&pagesize={1}&sort={2}&sortdirection={3}&userId={4}", pageIndex, pageSize, EscapeParameter(sortField), GetSortDirection(sortDirection), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<Tag>>(response);
                }
            }
        }

        public async Task<Tag> GetTagAsync(Guid tagId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/contacts/tags/{0}?userId={1}", tagId, actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<Tag>(response);
                }
            }
        }

        public async Task UpdateTagAsync(Tag tag, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/contacts/tags/{0}?userId={1}", tag.Id, actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(tag, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteTagAsync(Guid tagId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/contacts/tags/{0}", tagId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region Permissions

        public async Task<List<Permission>> GetPermissionsAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/permissions")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<Permission>>(response);
                }
            }
        }

        public async Task<List<Permission>> GetUserPermissionsAsync(long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/permissions/users/{0}", userId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<Permission>>(response);
                }
            }
        }

        public async Task UpdateUserPermissionsAsync(long userId, List<long> permissionIds) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/permissions/users/{0}", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(permissionIds, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task UpdatePermissionUsersAsync(long permissionId, List<long> userIds, bool granted) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/permissions/{0}/users?granted={1}", permissionId, granted))) {
                message.Content = new StringContent(JsonSerializer.Serialize(userIds, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region PhoneNumbers

        public async Task<List<PhoneNumber>> GetPhoneNumbersAsync(string type, bool? assigned) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/phonenumbers?type={0}&assigned={1}", EscapeParameter(type), assigned))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<PhoneNumber>>(response);
                }
            }
        }

        public async Task<PhoneNumber> GetPhoneNumberAsync(long phoneNumberId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/phonenumbers/{0}", phoneNumberId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<PhoneNumber>(response);
                }
            }
        }

        #endregion

        #region FmcPhones

        public async Task CreateFmcPhoneAsync(FmcPhone fmcPhone, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/fmcPhones?userId={0}", actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(fmcPhone, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<FmcPhone>> GetFmcPhonesAsync(long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/fmcPhones?userId={0}", actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<FmcPhone>>(response);
                }
            }
        }

        public async Task<FmcPhone> GetFmcPhoneAsync(string fmcPhoneId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/fmcPhones/{0}?userId={1}", EscapeParameter(fmcPhoneId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<FmcPhone>(response);
                }
            }
        }

        public async Task UpdateFmcPhoneAsync(FmcPhone fmcPhone, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/fmcPhones/{0}?userId={1}", EscapeParameter(fmcPhone.Id), actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(fmcPhone, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteFmcPhoneAsync(string fmcPhoneId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/fmcPhones/{0}?userId={1}", EscapeParameter(fmcPhoneId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region Groups

        public async Task CreateGroupAsync(Group group) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, "rest/groups")) {
                message.Content = new StringContent(JsonSerializer.Serialize(group, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<GroupListItem>> GetGroupsAsync(string searchTerm) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/groups?searchTerm={0}", EscapeParameter(searchTerm)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<GroupListItem>>(response);
                }
            }
        }

        public async Task<Group> GetGroupAsync(long groupId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/groups/{0}", groupId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<Group>(response);
                }
            }
        }

        public async Task UpdateGroupAsync(Group group) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, "rest/groups")) {
                message.Content = new StringContent(JsonSerializer.Serialize(group, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteGroupAsync(long groupId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/groups/{0}", groupId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region VoiceMailbox

        public async Task CreateVoiceMailboxAsync(VoiceMailbox voiceMailbox) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, "rest/voicemailboxes")) {
                message.Content = new StringContent(JsonSerializer.Serialize(voiceMailbox, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<VoiceMailboxListItem>> GetVoiceMailboxesAsync(string searchTerm) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/voicemailboxes?searchTerm={0}", EscapeParameter(searchTerm)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<VoiceMailboxListItem>>(response);
                }
            }
        }

        public async Task<VoiceMailbox> GetVoiceMailboxAsync(long voiceMailboxId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/voicemailboxes/{0}", voiceMailboxId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<VoiceMailbox>(response);
                }
            }
        }

        public async Task UpdateVoiceMailboxAsync(VoiceMailbox voiceMailbox) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, "rest/voicemailboxes")) {
                message.Content = new StringContent(JsonSerializer.Serialize(voiceMailbox, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteVoiceMailboxAsync(long voiceMailboxId) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/voicemailboxes/{0}", voiceMailboxId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region CallServices

        public async Task<List<CallService>> GetCallServicesAsync(string type) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/callservices?type={0}", EscapeParameter(type)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<CallService>>(response);
                }
            }
        }

        public async Task<CallService> GetCallServiceAsync(long callServiceId) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/callservices/{0}", callServiceId))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<CallService>(response);
                }
            }
        }

        #endregion

        #region ConferenceConfiguration

        public async Task<ConferenceConfiguration> GetConferenceConfigurationAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/conferenceConfiguration")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ConferenceConfiguration>(response);
                }
            }
        }

        public async Task UpdateConferenceConfigurationAsync(ConferenceConfiguration conferenceConfiguration, long userId) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/conferenceConfiguration?userId={0}", userId))) {
                message.Content = new StringContent(JsonSerializer.Serialize(conferenceConfiguration, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<ConferenceConfigurationTexts> GetConferenceEmailTextsAsync(string locale) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/conferenceConfiguration/defaults?locale={0}", EscapeParameter(locale)))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<ConferenceConfigurationTexts>(response);
                }
            }
        }

        #endregion

        #region Redirects

        public async Task<List<Redirection>> GetRedirectsAsync(long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/redirects?actOnBehalfOf={0}", actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<Redirection>>(response);
                }
            }
        }

        public async Task<Redirection> GetRedirectAsync(string redirectId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/redirects/{0}?actOnBehalfOf={1}", EscapeParameter(redirectId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<Redirection>(response);
                }
            }
        }

        public async Task UpdateRedirectAsync(Redirection redirection, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/redirects/{0}?actOnBehalfOf={1}", EscapeParameter(redirection.Id), actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(redirection, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        #endregion

        #region FunctionKeySets

        public async Task<List<FunctionKeySet>> GetFunctionKeySetsAsync(long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets?actOnBehalfOf={0}", actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<FunctionKeySet>>(response);
                }
            }
        }

        public async Task CreateFunctionKeyAsync(string functionKeySetId, FunctionKey functionKey, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Post, string.Format("rest/functionkeysets/{0}?actOnBehalfOf={1}", EscapeParameter(functionKeySetId), actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(functionKey, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<FunctionKey>> GetFunctionKeysAsync(string functionKeySetId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets/{0}?actOnBehalfOf={1}", EscapeParameter(functionKeySetId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<FunctionKey>>(response);
                }
            }
        }

        public async Task<FunctionKey> GetFunctionKeyAsync(string functionKeySetId, string keyId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets/{0}/{1}?actOnBehalfOf={2}", EscapeParameter(functionKeySetId), EscapeParameter(keyId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<FunctionKey>(response);
                }
            }
        }

        public async Task UpdateFunctionKeysAsync(string functionKeySetId, List<FunctionKey> functionKeys, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/functionkeysets/{0}?actOnBehalfOf={1}", EscapeParameter(functionKeySetId), actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(functionKeys, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task UpdateFunctionKeyAsync(string functionKeySetId, FunctionKey functionKey, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets/{0}/{1}?actOnBehalfOf={2}", EscapeParameter(functionKeySetId), EscapeParameter(functionKey.Id), actOnBehalfOf))) {
                message.Content = new StringContent(JsonSerializer.Serialize(functionKey, _jsonSerializerOptions), Encoding.UTF8, "application/json");
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task DeleteFunctionKeyAsync(string functionKeySetId, string keyId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("rest/functionkeysets/{0}/{1}?actOnBehalfOf={2}", EscapeParameter(functionKeySetId), EscapeParameter(keyId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    await CheckErrorAsync(response);
                }
            }
        }

        public async Task<List<string>> GetFunctionKeySetPhonesAsync(string functionKeySetId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets/{0}/phones?actOnBehalfOf={1}", EscapeParameter(functionKeySetId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<string>>(response);
                }
            }
        }

        public async Task<EditFunctionKey> GetEditFunctionKeyDefaultsAsync() {
            using (var message = new HttpRequestMessage(HttpMethod.Get, "rest/functionkeysets/edit/defaults")) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<EditFunctionKey>(response);
                }
            }
        }

        public async Task<EditFunctionKey> GetEditFunctionKeyAsync(string functionKeySetId, string keyId, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Get, string.Format("rest/functionkeysets/{0}/edit/{1}?actOnBehalfOf={2}", EscapeParameter(functionKeySetId), EscapeParameter(keyId), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<EditFunctionKey>(response);
                }
            }
        }

        public async Task<List<string>> ProvisionPhoneAsync(string functionKeySetId, string phone, long? actOnBehalfOf) {
            using (var message = new HttpRequestMessage(HttpMethod.Put, string.Format("rest/functionkeysets/{0}/phone?phone={1}actOnBehalfOf={2}", EscapeParameter(functionKeySetId), EscapeParameter(phone), actOnBehalfOf))) {
                using (var response = await _httpClient.SendAsync(message)) {
                    return await ResultAsync<List<string>>(response);
                }
            }
        }

        #endregion
    }
}
