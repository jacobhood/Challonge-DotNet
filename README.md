# Challonge.NET

Challonge.NET is a C# implementation of the Challonge! API built on .NET.

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
Initialize a client:

```C#
class Program
{
    private static readonly HttpClient _httpClient = new();
    private static readonly ChallongeCredentials _credentials = new("username", "apiKey");
    private static readonly ChallongeClient _client = new(_httpClient, _credentials);
	
    static void Main(string[] args)
    {
        var tournaments = _client.GetTournamentsAsync().Result;
        // Do more things
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
}
```
