# Challonge.NET

Challonge.NET is a C# implementation of the CHALLONGE! API built on .NET.

## Requirements

- .NET 5.0

## Installation

The library is available through NuGet. Install as you would any other package.

## Usage

#### Console Application

Add these `using` directives to your `Program.cs` file:

```C#
using Challonge.Api;
using Challonge.Objects;
```
Create and use a client:

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
#### ASP.NET Core Web Application

Challonge.NET supports dependency injection in ASP.NET Core web applications using the built-in dependency injection framework.

In `Startup.cs`, add:
```C#
using Challonge.Extensions;
```
Then, in `ConfigureServices`:
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddChallonge("username", "apiKey");
    // Consider storing your credentials outside your source code 
    // (e.g. as environment variables, in a configuration file, etc.)

}
```
Now, add the appropriate `using` directives and inject `IChallongeClient` into your controllers:

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

You can also inject `IChallongeCredentials` to access the username and api key being used to interact with the API.