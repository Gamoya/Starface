# Starface
An aot compatible .NET client for Starface

[![NuGet](https://img.shields.io/nuget/v/Gamoya.Phone.Starface)](https://www.nuget.org/packages/Gamoya.Phone.Starface)
[![GitHub](https://img.shields.io/github/license/Gamoya/Starface)](https://github.com/Gamoya/Starface/blob/main/LICENSE)

## Usage

```C#

var apiUrl = "{your api url}";
var apiUsername = "{your username}";
var apiPassword = "{your password}";

using (var starfaceClient = new StarfaceClient(apiUrl)) {
    await starfaceClient.SignInAsync(apiUsername, apiPassword);

    var userMe = await starfaceClient.GetMeAsync();
    Console.WriteLine(string.Format("Me: {0} {1}", userMe.FirstName, userMe.FamilyName));
}

```
