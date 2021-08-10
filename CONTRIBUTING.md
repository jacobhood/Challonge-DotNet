# How to contribute

## Changes

If you'd like to propose a change to Challonge-DotNet, please create an issue in this repository detailing the enhancement/fix.  

### Contributing

1. Select an unassigned issue, or create a new one.
2. Assign the issue to yourself.
3. Implement the change on your branch.
4. Submit a pull request with the issue number in the description.

### Testing

In a perfect world, any change in functionality would be accompanied by at least one test in the ChallongeTests project.
In this world, exceptions may be made for minor changes, but the associated issues will be labeled "untested" for tracking 
since the goal is complete test coverage. Ensure any test `Tournament`s you create end with the suffix provided in the source 
code (`_testTournamentSuffix`). The cleanup process uses it to identify `Tournament`s created during test execution.

## Style

Follow the standard [Microsoft C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) with the modifications below:

- Always specify the type during variable initialization, even if the type is obvious from the value being assigned (i.e. avoid `var`).
- Prefer [target-typed `new` expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new) to explicit constructors only 
when the type of the object being instantiated is clear. Some subjective judgment required.