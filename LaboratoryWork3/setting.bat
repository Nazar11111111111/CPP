cd Algorithms
dotnet pack -o ..\NuGetRepository
rmdir bin obj /s /q
cd ..\ConsoleApp
dotnet build