---
uid: guides_gettingStarted
---

# Getting started
Once you've installed the package (see [homepage](xref:homepage)), you will need to retrieve 
your Challonge! API key, which you can find [here](https://challonge.com/settings/developer).

> [!CAUTION]
> This key allows direct interaction with the tournaments your account has access to.
> Do not share it with anyone else and protect it like you would a password.

Every API interaction happens through the `ChallongeClient`. You can either instantiate it normally 
or use dependency injection.

# [Console Application](#tab/console)
Add these `using` directives:
```c#
using System.Net.Http;
using System.Collections.Generic;
using Challonge.Api;
using Challonge.Objects;
```

Create the client and use its methods:
```c#
class Program
{
    private static readonly HttpClient _httpClient = new();
    private static readonly ChallongeCredentials _credentials = new("username", "apiKey");
    private static readonly ChallongeClient _client = new(_httpClient, _credentials);
	
    static void Main(string[] args)
    {
        IEnumerable<Tournament> tournaments = _client.GetTournamentsAsync().Result;
    }
}
```

# [ASP.NET Core Web Application](#tab/di)
Challonge-DotNet supports dependency injection using the built-in dependency injection framework.

In `Startup.cs`
```c#
using Challonge.Extensions.DependencyInjection;
```
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddChallonge("username", "apiKey");
}
```

Add the appropriate `using` directives to your controller file:
```c#
using Challonge.Api;
using Challonge.Objects;
```

Inject the client:
```c#
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

***

# Example
Assume we have an already-configured `ChallongeClient` `_client` as a field in our class.

```c#
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

> [!CAUTION]
> Remember that your api key is very sensitive. In the previous example, we copy-pasted the
> api key and Challonge! username into the code, which is NOT secure, especially if you
> plan on distributing the application in any shape or form. 
> 
> We recommend alternative storage such as environment variables, an external configuration 
> file, or a secrets manager for safe handling.
