For a more detailed output when running unit test:
dotnet test --logger "console;verbosity=normal"

To run run coverage and generate a html report:
Navigate to /TaskManager.Tests and run:
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=./TestResults/
reportgenerator "-reports:./TestResults/coverage.cobertura.xml" "-targetdir:./CoverageReport" -reporttypes:Html

To build and run the app, while in /TaskManager:
dotnet build
dotnet run
