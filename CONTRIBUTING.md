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
In this world, exceptions may be made for minor changes, though in these cases the issues will be labeled 
"untested" for tracking since the goal is complete test coverage. Some guidelines:

- Ensure any test `Tournament`s you create contain the string "test" (not capitalization-sensitive) and end with the suffix 
provided in the source code. This allows for easier cleanup in case tests fail.
- Don't reuse any test tournament names so as to prevent any name clashes with tournaments that haven't been deleted due to test failure.

## Style

Follow the standard [Microsoft C# coding conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) with the modifications below:

- Always specify the type during variable initialization, even if the type is obvious from the value being assigned (i.e. avoid `var`).
- Prefer [target-typed `new` expressions](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-9.0/target-typed-new) to explicit constructors only 
when the type of the object being instantiated is clear. Some subjective judgment required.