curl "https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip" ^
    -o BaGet.zip -L
mkdir BaGet
tar zxf BaGet.zip -C BaGet
del BaGet.zip
cd BaGet
start dotnet Baget.dll
cd ..\ConsoleApp
dotnet pack -o .
dotnet nuget push NLeonchuk.1.0.0.nupkg -s http://localhost:5000/v3/index.json
del NLeonchuk.1.0.0.nupkg
rmdir bin, obj /s /q
cd ..\LaboratoryWorks
rmdir bin, obj /s /q
cd ..
vagrant up windows
vagrant halt windows
vagrant up linux
vagrant halt linux
vagrant up mac
vagrant halt mac