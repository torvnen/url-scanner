# url-scanner
ASP.NET Core Web API project. Scans given input for URLs.

# Requirements
- Option 1: run on IIS Express
  - .NET Core 3 runtime
- Option 2: run on Docker
  - Docker

# Running the API
- Option 1: Visual Studio
  - Set API as Startup Project
  - Hit Debug
- Option 2: runner file
  - Execute the `run-on-iis.bat` file in the root folder to run on IIS Express
  - Execute the `run-on-docker.bat` file in the root folder to run on Docker

# Running tests
Running the tests requires .NET Core 3 runtime.

You may run the tests with `dotnet test` in the root folder
