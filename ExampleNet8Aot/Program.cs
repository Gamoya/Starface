namespace Gamoya.Phone.Starface.Example {
    internal class Program {
        static async Task Main() {
            var apiUrl = Environment.GetEnvironmentVariable("STARFACE_API_URL");
            var apiUsername = Environment.GetEnvironmentVariable("STARFACE_API_USERNAME");
            var apiPassword = Environment.GetEnvironmentVariable("STARFACE_API_PASSWORD");

            using (var starfaceClient = new StarfaceClient(apiUrl)) {
                await starfaceClient.SignInAsync(apiUsername, apiPassword);

                var userMe = await starfaceClient.GetMeAsync();
                Console.WriteLine(string.Format("Me: {0} {1}", userMe.FirstName, userMe.FamilyName));

                var phoneConfig = await starfaceClient.GetPhoneConfigAsync(userMe.Id);
                Console.WriteLine(string.Format("PhoneConfig - DoNotDisturb: {0} / CallWaiting: {1}", phoneConfig.DoNotDisturb, phoneConfig.CallWaiting));

                var phoneAssignments = await starfaceClient.GetPhoneAssignmentsAsync(userMe.Id);
                foreach (var phoneAssignment in phoneAssignments) {
                    Console.WriteLine(string.Format("PhoneAssignment: {0} - {1}", phoneAssignment.PhoneType, phoneAssignment.PhoneName));
                    var phoneAssignmentNumbers = await starfaceClient.GetPhoneAssignmentNumbersAsync(userMe.Id, phoneAssignment.PhoneId);
                    foreach (var phoneAssignmentNumber in phoneAssignmentNumbers) {
                        Console.WriteLine(string.Format("PhoneAssignmentNumber: {0} - {1}", phoneAssignmentNumber.PhoneNumber, phoneAssignmentNumber.Active));
                    }
                }

                var users = await starfaceClient.GetUsersAsync(null);
                foreach (var user in users) {
                    Console.WriteLine(string.Format("User: {0} {1}", user.FirstName, user.FamilyName));
                }

                var functionKeySets = await starfaceClient.GetFunctionKeySetsAsync(null);
                foreach (var functionKeySet in functionKeySets) {
                    Console.WriteLine(string.Format("FunctionKeySet: {0} - {1}", functionKeySet.Id, functionKeySet.Name));
                    var functionKeys = await starfaceClient.GetFunctionKeysAsync(functionKeySet.Id, null);
                    foreach (var functionKey in functionKeys) {
                        Console.WriteLine(string.Format("FunctionKey: {0} - {1}", functionKey.Id, functionKey.Name));
                    }
                }

                var fmcPhones = await starfaceClient.GetFmcPhonesAsync(null);
                foreach (var fmcPhone in fmcPhones) {
                    Console.WriteLine(string.Format("FmcPhone: {0} - {1}", fmcPhone.Id, fmcPhone.Number));
                }

                var accounts = await starfaceClient.GetAccountsAsync();
                foreach (var account in accounts) {
                    Console.WriteLine(string.Format("Account: {0} - {1} {2}", account.Id, account.FirstName, account.LastName));
                }

                var tags = await starfaceClient.GetTagsAsync(0, null, null, System.ComponentModel.ListSortDirection.Ascending, null);
                foreach (var tag in tags) {
                    Console.WriteLine(string.Format("Tag: {0} - {1} / {2} / {3}", tag.Id, tag.Name, tag.Alias, tag.Owner));
                }

                var contacts = await starfaceClient.GetContactsAsync(null, null, 0, null, null, System.ComponentModel.ListSortDirection.Ascending, null);
                foreach (var contact in contacts.Contacts) {
                    var contactDetails = await starfaceClient.GetContactAsync(contact.Id, null);
                    Console.WriteLine(string.Format("Contact: {0} - {1}", contact.Id, contact.SummaryValues.FirstOrDefault()));
                }

                var permissions = await starfaceClient.GetPermissionsAsync();
                foreach (var permission in permissions) {
                    Console.WriteLine(string.Format("Permission: {0} - {1}", permission.Name, permission.Description));
                }

                var phoneNumbers = await starfaceClient.GetPhoneNumbersAsync(null, null);
                foreach (var phoneNumber in phoneNumbers) {
                    Console.WriteLine(string.Format("PhoneNumber: {0} {1} {2}", phoneNumber.NationalPrefix, phoneNumber.LocalPrefix, phoneNumber.Number));
                }

                var groups = await starfaceClient.GetGroupsAsync(null);
                foreach (var group in groups) {
                    Console.WriteLine(string.Format("Group: {0} - {1} / {2} / {3}", group.Id, group.Name, group.InternalNumber, group.ExternalNumber));
                }

                var voiceMailboxes = await starfaceClient.GetVoiceMailboxesAsync(null);
                foreach (var voiceMailbox in voiceMailboxes) {
                    Console.WriteLine(string.Format("VoiceMailbox: {0} - {1} / {2}", voiceMailbox.Id, voiceMailbox.Name, voiceMailbox.Number));
                }

                var callServices = await starfaceClient.GetCallServicesAsync(null);
                foreach (var callService in callServices) {
                    Console.WriteLine(string.Format("CallService: {0} - {1} / {2}", callService.Id, callService.Name, callService.Label));
                }

                var redirects = await starfaceClient.GetRedirectsAsync(null);
                foreach (var redirect in redirects) {
                    Console.WriteLine(string.Format("Redirect: {0} - {1} / {2}", redirect.Id, redirect.PhoneNumber, redirect.GroupNumber));
                }

                var conferenceConfiguration = await starfaceClient.GetConferenceConfigurationAsync();
                Console.WriteLine(string.Format("ConferenceConfiguration: {0} / {1} / {2} / {3}", conferenceConfiguration.Language, conferenceConfiguration.InternalNumberId, conferenceConfiguration.ExternalNumberId, conferenceConfiguration.ServerAddress));

                var conferenceEmailTexts = await starfaceClient.GetConferenceEmailTextsAsync("en");
                Console.WriteLine(string.Format("ConferenceEmailTexts: {0} - {1}", conferenceEmailTexts.EmailSubject, conferenceEmailTexts.EmailBody));

                var contactScheme = await starfaceClient.GetContactSchemeAsync("en");
                Console.WriteLine(string.Format("ContactScheme: {0} / {1}", contactScheme.SummaryBlock.Name, contactScheme.PhoneNumbersBlock.Name));

                await starfaceClient.SignOutAsync();
            }
        }
    }
}
