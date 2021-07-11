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
    // Consider storing your credentials outside your source code 
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

## Notes

Disclaimer: These observations come from my own testing and may not be totally accurate.

- Match attachment files must be images
- The result of `GetParticipantsAsync` after calling `UndoCheckInParticipantAsync` does not display the correct checked-in status, but the result of `UndoCheckInParticipantAsync` does.