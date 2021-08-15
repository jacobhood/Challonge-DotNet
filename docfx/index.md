---
uid: homepage
---

# Challonge-DotNet
[![LastCommit](https://img.shields.io/github/last-commit/jacobhood/Challonge-DotNet?style=flat&logo=github)](https://github.com/jacobhood/Challonge-DotNet)
[![Nuget](https://img.shields.io/nuget/v/Challonge-DotNet?style=flat&logo=nuget)](https://www.nuget.org/packages/Challonge-DotNet)
***
Challonge-DotNet is a C# implementation of the CHALLONGE! API built on .NET that exposes asynchronous
methods for interacting with the API.

## Requirements
***
- .NET 5.0
- Internet connection

## Installation
***
Recommended methods:
- Package manager UI
- Package manager Console:\
```PM> Install-Package Challonge-DotNet -Version 1.1.0```
- .NET CLI\
```> dotnet add package Challonge-DotNet --version 1.1.0```

## Basic Usage
***
See the [guides section](xref:guides_gettingStarted) to see some examples on how to use the package.

*** 
> [!NOTE]
> The following observations come from experimenting and debugging, so they may not be totally accurate.
> - Match attachment files must be images.
> - The `Participant` returned from `UndoCheckInParticipantAsync` has the correct checked-in status,
> but this is not the case for the `Participant` returned from `GetParticipantAsync` after undoing their check-in. 
> This behavior has been documented elsewhere, so there's a chance it's a Challonge-side issue.
