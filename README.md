# Challonge.NET

Challonge.NET is a C# implementation of the Challonge! API built on .NET.

## Requirements

- .NET 5.0

## Installation

The library is available through NuGet. Install as you would any other package.

## Usage

#### Console Application

Add a `using Challonge.Api` statement to your `Program.cs` file. You'll probably want a `using Challonge.Objects` statement too.

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
