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
    PM> Install-Package Challonge-DotNet -Version 1.0.0-beta2
    ```

- .NET CLI
    ```
    > dotnet add package Challonge-DotNet --version 1.0.0-beta2
    ```

## Usage

### Console Application

Add these `using` directives:

```C#
using System.Net.Http;
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
	
    static void Main(string[] args)
    {
        var tournaments = _client.GetTournamentsAsync().Result;
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
You can also inject `IChallongeCredentials` to access the username and api key being used by the client.

### Basic Functionality

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

## Notes

These observations come from my own testing and may not be totally accurate.

- Match attachment files must be images.
- The `Participant` returned from `UndoCheckInParticipantAsync` has the correct checked-in status, but this is not the case for the `Participant` returned from `GetParticipantAsync` after undoing their check-in.
 This behavior has been documented [elsewhere](https://github.com/ZEDGR/pychallonge#api-issues), so there's a chance it's a Challonge-side issue.
- Some of the enums are almost certainly incomplete. For example, I couldn't find the current possible tournament states explicitly listed anywhere, so the `TournamentState` enum may be missing values. 
I'll provide updates as discrepancies are discovered.