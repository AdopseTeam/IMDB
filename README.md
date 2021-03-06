## IMDB Clone
[![Maintenance](https://img.shields.io/badge/Maintained%3F-yes-green.svg)](https://GitHub.com/AdopseTeam/IMDB/graphs/commit-activity)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)
![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)
![Hits](https://hitcounter.pythonanywhere.com/count/tag.svg?url=https://github.com/AdopseTeam/IMDB)


### Contributors
![GitHub Contributors Image](https://contrib.rocks/image?repo=AdopseTeam/IMDB)


### Application Tech Stack
<img align="left" alt="C#"  width="50px" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/csharp/csharp.png" /> 
<img align="left" alt=".Net"  width="50px" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/dotnet/dotnet.png" /> 
<img align="left" alt="Bootstrap"  width="50px" src="https://raw.githubusercontent.com/github/explore/80688e429a7d4ef2fca1e82350fe8e3517d3494d/topics/bootstrap/bootstrap.png" /> 

<br/>
<br/>

### Installation/Setup

#### Using VSCode

First of all you need to have installed [ASP.NET Core](https://dotnet.microsoft.com/download) Core and [VSCode](https://code.visualstudio.com/download)

Then you need to install some tool by running the following commands:

``` bash
dotnet tool install --global dotnet-ef
```

### Launching the web app:
```bash
dotnet run
```

### Using VisualStudio:

First, make sure you have installed [ASP.NET Core](https://dotnet.microsoft.com/download) Core and [Visual Studio](https://visualstudio.microsoft.com/vs/)

After cloning or downloading the project, ensure the tool EF was already installed. You can find it by running the command:

``` bash
dotnet tool install --global dotnet-ef
```

Open a command prompt and execute the following commands:

```bash
dotnet restore
dotnet tool restore
```

Select the "IMDB.csproj" as startup item and run the application 

### Launching the desktop app:
In order to launch the desktop application you need [NODE](https://nodejs.org/en/download/) installed.

Then you need to add the Electron.Net package by running:
```bash
dotnet tool install ElectronNET.CLI -g
dotnet restore
```

Then you can run the desktop app:
```bash
electronize start
```

You can build the desktop app by running:
```bash
electronize build /target win  (Windows)
electronize build /target linux (Linux)
electronize build /target osx (Mac)
```
