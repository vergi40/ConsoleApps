# ConsoleApps
Console app utilities for automations.

Dependencies:
* .NET8
* Built upon the [ConsoleAppFramework](https://github.com/Cysharp/ConsoleAppFramework)

Building:
* Clone repository
* Build solution on VS or `dotnet build`
* Use the binary `.dll` or `.exe` via command line. List available functionality with `--help`

## CsUtils
Utilities for C# project structure. Reads `.sln` file to output information about it's project `.csproj` files. 
```
$ ./CsUtils.exe --help
Usage: [command] [-h|--help] [--version]

Commands:
  print    Print all project file (.csproj) paths from solution file (.sln).
```

```
$ ./CsUtils.exe print --help
Usage: print [arguments...] [options...] [-h|--help] [--version]

Print all project file (.csproj) paths from solution file (.sln).

Arguments:
  [0] <string>    Full path to solution file (.sln)

Options:
  --type|--output-type <OutputType>     Control the information to print. Full|RelativePath|FullPath|Name (Default: Full)
  --tests|--only-tests                  Include only test projects. (Optional)
  --tfm|--target-framework <string?>    Filter result to e.g. only contain "net8" projects. (Default: null)
  -v|--verbose                           (Optional)
```
