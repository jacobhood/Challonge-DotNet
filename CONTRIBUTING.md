# How to contribute

## Changes

If you'd like to propose a change to Challonge-DotNet, please create an issue in this repository detailing the enhnacement or fix.
To contribute, select an issue, and when you're finished working the issue on your branch, submit a pull request with the corresponding issue number in the description.

## Style

Follow the standard [Microsoft C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) with the modifications below:

* Always specify the type on the left side of the assignment when initializing a variable, even if the type is obvious from the value being assigned (i.e. avoid `var`).
* Prefer [target-typed `new` expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new) to explicit constructors for variable initialization.