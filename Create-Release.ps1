Push-Location .\src

$releaseFolder = "..\release"
$releaseFoldertmp = "$releaseFolder\tmp"
$zipArchive = "cspreportfunction.zip"

if (Test-Path $releaseFolder) { Remove-Item $releaseFolder -Force -Recurse }

# Build Section
dotnet publish cspreportfunction.sln -c Release -f netcoreapp3.1 --nologo -o $releaseFoldertmp #-r linux-x64
Compress-Archive -Path "$releaseFoldertmp\*" -DestinationPath "$releaseFolder\$zipArchive" -force
remove-item -Force -Recurse $releaseFoldertmp

# Test Section
dotnet test cspreportfunction.sln -c Release -f netcoreapp3.1 --nologo -o $releaseFoldertmp #-r linux-x64

Pop-Location