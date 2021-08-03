# How to contribute

## Changes

If you'd like to propose a change to Challonge-DotNet, please create an issue in this repository detailing the enhancement/fix.  
To contribute:  

1. Select an unassigned issue, or create a new one
2. Assign the issue to yourself
3. Implement the change on your branch
4. Submit a pull request with the issue number in the description

## Style

Follow the standard [Microsoft C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) with the modifications below:

* Always specify the type during variable initialization, even if the type is obvious from the value being assigned (i.e. avoid `var`).
* Prefer [target-typed `new` expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new) to explicit constructors only when the type is given on the left side of the assignment.