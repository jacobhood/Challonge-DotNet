# Challonge.NET

Challonge.NET is a C# implementation of the [CHALLONGE! API](https://api.challonge.com/v1) 
built on .NET that exposes asynchronous methods for interacting with the API.

## Requirements

- .NET 5.0
- Internet connection

## Installation

The library is available through NuGet. Install as you would any other package.

## Usage

### Console Application

Add these `using` directives:

```C#
using Challonge.Api;
using Challonge.Objects;
using System.Net.Http;
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

Challonge.NET supports dependency injection using the built-in dependency injection framework.

In `Startup.cs`:
```C#
using Challonge.Extensions.DependencyInjection;
```
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddChallonge("username", "apiKey");
    // Consider storing credentials outside your source code
}
```
Add the appropriate `using` directives to your controller:

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

### Basic Functionalities

Assume we have an already-configured `ChallongeClient` `_client` as a field in our class. Instantiate a `TournamentInfo` object:

```C#
    public async Task<Tournament> TournamentExample()
    {
        IEnumerable<Participant> participants = new List<Participant>();
        TournamentInfo info = new()
        {
            AcceptAttachments = True,
            TournamentType = TournamentType.DoubleElimination,
            Name = "SSBM Weekly #46"
        };
        Tournament tournament = await _client.CreateTournamentAsync(info);

        for (int i = 0; i < 5; i++)
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
            ParticipantInfo pInfo = new();
            pInfo.Misc = p.Seed % 2 == 0 ? "Even seed" : "Odd seed";
            await _client.UpdateParticipantAsync(p, pInfo);
        }

        tournament = await _client.GetTournamentByIdAsync(tournament.Id);
        return tournament;
    }
```

## Notes

These observations come from my own testing and may not be totally accurate.

- Match attachment files must be images
- The `Participant` returned from `UndoCheckInParticipantAsync` has the correct checked-in status, but this is not the case for the `Participant` returned from `GetParticipantAsync`.
 This behavior has been documented by authors of other implementations of this API and is most likely a Challonge-side issue.
- The current set of possible values of a tournament's state is unknown, but further testing would probably yield some insight. I'll release an update if I figure this out.