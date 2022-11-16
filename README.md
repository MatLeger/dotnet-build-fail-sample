# dotnet-build-fail-sample
This is a sample project for https://github.com/dotnet/sdk/issues/29088

### Describe the bug
Building a solution with `dotnet build` fails when that solution has a C# vcproj that has a project reference to a C++/CLI vcxproj. The solution will build successfully and run in Visual Studio Professional 2022 v17.3.6 but if the solution is built using the dotnet CLI then it fails. If the vcxproj is removed from the solution and the project references removed and the referenced code commented out, then `dotnet build` works.

I have attached a very simple "Hello World" solution to demonstrate the problem. It's an empty Web App that simply outputs "Hello World! (From C++/CLI)". The output string comes from a class defined in the C++/CLI project referenced by the Web App.

I've also created a repo for it:
[https://github.com/MatLeger/dotnet-build-fail-sample](https://github.com/MatLeger/dotnet-build-fail-sample)

This is the output from dotnet build:
```
**********************************************************************
** Visual Studio 2022 Developer PowerShell v17.3.6
** Copyright (c) 2022 Microsoft Corporation
**********************************************************************
PS C:\code\dotnet-build-fail-sample\WebApplication1> dotnet build
MSBuild version 17.3.2+561848881 for .NET
C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj : warning NU1503: Skipping restore for project 'C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj'. The project file may be invalid or missing targets required for restore. [C:\code\dotnet-build-fail-sample\WebApplication1\WebApplication1.sln]
  Determining projects to restore...
  All projects are up-to-date for restore.
C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj(30,3): error MSB4019: The imported project "C:\Microsoft.Cpp.Default.props" was not found. Confirm that the expression in the Import declaration "\Microsoft.Cpp.Default.props" is correct, and that the file exists on disk.
C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj(30,3): error MSB4019: The imported project "C:\Microsoft.Cpp.Default.props" was not found. Confirm that the expression in the Import declaration "\Microsoft.Cpp.Default.props" is correct, and that the file exists on disk.

Build FAILED.

C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj : warning NU1503: Skipping restore for project 'C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj'. The project file may be invalid or missing targets required for restore. [C:\code\dotnet-build-fail-sample\WebApplication1\WebApplication1.sln]
C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj(30,3): error MSB4019: The imported project "C:\Microsoft.Cpp.Default.props" was not found. Confirm that the expression in the Import declaration "\Microsoft.Cpp.Default.props" is correct, and that the file exists on disk.
C:\code\dotnet-build-fail-sample\WebApplication1\Project1\Project1.vcxproj(30,3): error MSB4019: The imported project "C:\Microsoft.Cpp.Default.props" was not found. Confirm that the expression in the Import declaration "\Microsoft.Cpp.Default.props" is correct, and that the file exists on disk.
    1 Warning(s)
    2 Error(s)

Time Elapsed 00:00:00.86
```

### To Reproduce
- Clone the repo at https://github.com/MatLeger/dotnet-build-fail-sample or see attached zip file. 
- Open the solution in Visual Studio 2022
- Run the solution in VS and it will build and then show a web page with "Hello World! (From C++/CLI)"
- Open a developer command prompt and try to build the solution with dotnet build and it will fail

**C++/CLI Project Code**
```
namespace Project1
{
    public ref class HelloWorld
    {
    public:
        static property System::String^ Salutations { System::String^ get() { return "Hello World! (From C++/CLI)"; } }
    };
}
```
**ASP.Net Core Startup code (Program.cs)**
```
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Project1.HelloWorld.Salutations);

app.Run();
```

### Exceptions (if any)
No exceptions thrown. The `dotnet build` command fails.

### Further technical details
**IDE**
Visual Studio Professional 2022 (64-bit)
Version 17.3.6

**MSBuild Version**
```
C:\code\dotnet-build-fail-sample\WebApplication1>msbuild -version
MSBuild version 17.3.1+2badb37d1 for .NET Framework
17.3.1.41501
```

**Output of `dotnet --info`**
```
C:\code\dotnet-build-fail-sample\WebApplication1>dotnet --info
.NET SDK (reflecting any global.json):
 Version:   6.0.402
 Commit:    6862418796

Runtime Environment:
 OS Name:     Windows
 OS Version:  10.0.19044
 OS Platform: Windows
 RID:         win10-x64
 Base Path:   C:\Program Files\dotnet\sdk\6.0.402\

global.json file:
  Not found

Host:
  Version:      6.0.11
  Architecture: x64
  Commit:       943474ca16

.NET SDKs installed:
  3.1.425 [C:\Program Files\dotnet\sdk]
  5.0.104 [C:\Program Files\dotnet\sdk]
  5.0.408 [C:\Program Files\dotnet\sdk]
  5.0.414 [C:\Program Files\dotnet\sdk]
  6.0.203 [C:\Program Files\dotnet\sdk]
  6.0.402 [C:\Program Files\dotnet\sdk]

.NET runtimes installed:
  Microsoft.AspNetCore.All 2.1.30 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.All]
  Microsoft.Asp
[WebApplication1.zip](https://github.com/dotnet/sdk/files/10024176/WebApplication1.zip)
NetCore.App 2.1.30 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 3.1.31 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.4 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.13 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 5.0.17 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.5 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.10 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.AspNetCore.App 6.0.11 [C:\Program Files\dotnet\shared\Microsoft.AspNetCore.App]
  Microsoft.NETCore.App 2.1.30 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 3.1.31 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.4 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.13 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 5.0.17 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.5 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.9 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.10 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.NETCore.App 6.0.11 [C:\Program Files\dotnet\shared\Microsoft.NETCore.App]
  Microsoft.WindowsDesktop.App 3.1.31 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 5.0.4 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 5.0.13 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 5.0.17 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 6.0.5 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 6.0.10 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]
  Microsoft.WindowsDesktop.App 6.0.11 [C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App]

Download .NET:
  https://aka.ms/dotnet-download

Learn about .NET Runtimes and SDKs:
  https://aka.ms/dotnet/runtimes-sdk-info
```
