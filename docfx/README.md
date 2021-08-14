# Challonge-DotNet documentation
***
The documentation is built using the [docfx](https://dotnet.github.io/docfx/) CLI.
It automatically parses the xml documentation of the source code.

## Extending the documentation
***
You can extend the documentation by writing markdown files, like described 
[here](https://dotnet.github.io/docfx/spec/docfx_flavored_markdown.html?tabs=tabid-1%2Ctabid-a).

**IMPORTANT: The parsing engine used for this project is [markdig](https://github.com/lunet-io/markdig),
the default setting for docfx. It should not be changed.**

## Building the static html documentation
***
Follow the [installation guide for docfx CLI](https://dotnet.github.io/docfx/tutorial/docfx_getting_started.html#2-use-docfx-as-a-command-line-tool).
Once installed, from the project root, run 
```
> docfx docfx/docfx.json
```
This will build the documentation and output the static html page inside the `docs` folder.