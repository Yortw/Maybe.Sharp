del /F /Q /S *.CodeAnalysisLog.xml

"..\.nuget\NuGet.exe" pack -sym Maybe.Sharp.nuspec -BasePath .\
pause

copy *.nupkg C:\Nuget.LocalRepository\
pause
