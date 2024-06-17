# Challonge-DotNet

Challonge-DotNet is a C# implementation of the [CHALLONGE! API](https://api.challonge.com/v1) 
built on .NET that exposes asynchronous methods for interacting with the API.

## Requirements

- .NET 5.0
- Internet connection

## Installation

Recommended methods:

- Package Manager UI

- Package Manager Console
    ```
    PM> Install-Package Challonge-DotNet -Version 3.0.1
    ```

- .NET CLI
    ```
    > dotnet add package Challonge-DotNet --version 3.0.1
    ```

## Usage

### Console Application

Add these `using` directives:

```C#
using System.Net.Http;
using System.Collections.Generic;
using Challonge.Api;
using Challonge.Objects;
```
Create the client and use its methods:

```C#
class Program
{
    private static readonly HttpClient _httpClient = new();
    private static readonly ChallongeCredentials _credentials = new("username", "apiKey");
    private static readonly ChallongeClient _client = new(_httpClient, _credentials);
	
    static void Main()
    {
        IEnumerable<Tournament> tournaments = _client.GetTournamentsAsync().Result;
    }
}
```

### ASP.NET Core Web Application

Challonge-DotNet supports dependency injection using the built-in dependency injection framework.

In `Startup.cs`:
```C#
using Challonge.Extensions.DependencyInjection;
```
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddChallonge("username", "apiKey");
}
```
Add the appropriate `using` directives to your controller file:

```C#
using Challonge.Api;
using Challonge.Objects;
```

Inject the client:

```C#
public class HomeController : Controller
{
    private readonly IChallongeClient _client;

    public HomeController(IChallongeClient client)
    {
        _client = client;
    }
}
```
You can also inject `IChallongeCredentials` to access the username and api key used by the client.

### Example

Assume we have an already-configured `ChallongeClient` `_client` as a field in our class.

```C#
public async Task<Tournament> Example()
{
    TournamentInfo info = new()
    {
        AcceptAttachments = true,
        TournamentType = TournamentType.DoubleElimination,
        Name = "SSBM Weekly #46"
    };
    Tournament tournament = await _client.CreateTournamentAsync(info);

    for (int i = 1; i <= 5; i++)
    {
        ParticipantInfo pInfo = new()
        {
            Name = $"player{i}"
        };
        await _client.CreateParticipantAsync(tournament, pInfo);
    }

    IEnumerable<Participant> participants = await _client.RandomizeParticipantsAsync(tournament);

    foreach (Participant p in participants)
    {
        ParticipantInfo pInfo = new()
        {
            Misc = p.Seed % 2 == 0 ? "Even seed" : "Odd seed";
        };
        await _client.UpdateParticipantAsync(p, pInfo);
    }

    tournament = await _client.GetTournamentByIdAsync(tournament.Id);
    return tournament;
}
```

## Tests

The ChallongeTests project contains a minimal set of tests that validate the 
core functionality of Challonge-DotNet. Visual Studio 2019 is strongly recommended
for navigating and running the tests.

First, create CHALLONGE_USERNAME and CHALLONGE_API_KEY environment variables and set them to the 
appropriate values. Then, run the tests using your preferred method. If you find that the built-in 
cleanup doesn't execute properly, use this program to remove any residual test tournaments:

```C#
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Challonge.Api;
using Challonge.Objects;

namespace DeleteTestTournaments
{
    class Program
    {
        private static readonly ChallongeCredentials _credentials = new(
            Environment.GetEnvironmentVariable("CHALLONGE_USERNAME"), 
            Environment.GetEnvironmentVariable("CHALLONGE_API_KEY"));
        private static readonly ChallongeClient _client = new(new HttpClient(), _credentials);

        static async Task Main()
        {
            foreach(Tournament t in await _client.GetTournamentsAsync())
            {
                if (t.Name.EndsWith("-_-_Challonge-DotNetTest-_-_"))
                {
                    await _client.DeleteTournamentAsync(t);
                }
            }
        }
    }
}
```
  
Coverage is not yet complete, and until then, the test project will remain under active development.
It will also be updated as needed for testing any changes to the library.

## Notes

These observations arose from experimenting and debugging, so they may not be totally accurate.

- Match attachment files must be images.
- The `Participant` returned from `UndoCheckInParticipantAsync` has the correct checked-in status, but this is not the case for the `Participant` 
returned from `GetParticipantAsync` after undoing their check-in. This behavior has been documented [elsewhere](https://github.com/ZEDGR/pychallonge#api-issues), 
so there's a chance it's a Challonge-side issue.
- Some of the enums are almost certainly incomplete. For example, I couldn't find an up-to-date list of tournament states, so I don't know if the 
`TournamentState` enum contains all possible values, nor if it contains invalid values. I'll provide updates as discrepancies are discovered.
- The `HasAttachment` property of `Match` does not appear to be set to true when a `MatchAttachment` is created; however, the `AttachmentCount` property
is incremented.