[CmdletBinding(PositionalBinding=$false)]
param(
    [bool] $CreatePackages,
    [bool] $RunTests = $true,
    [string] $PullRequestNumber
)

Write-Host 'OSArchitecture: ' (Get-WmiObject Win32_OperatingSystem).OSArchitecture
$exePath = "$env:USERPROFILE\SSCERuntime.exe"

If ((Get-WmiObject Win32_OperatingSystem).OSArchitecture -eq '64-bit') {
    (New-Object Net.WebClient).DownloadFile('https://download.microsoft.com/download/F/F/D/FFDF76E3-9E55-41DA-A750-1798B971936C/ENU/SSCERuntime_x64-ENU.exe', $exePath )
} Else {
    (New-Object Net.WebClient).DownloadFile('https://download.microsoft.com/download/F/F/D/FFDF76E3-9E55-41DA-A750-1798B971936C/ENU/SSCERuntime_x86-ENU.exe', $exePath)
}

$destPath = "$env:USERPROFILE\SSCERuntime"
Write-Host "Unpacking..."
7z x $exePath -o"$destPath" | Out-Null

Write-Host "Installing..."

If ((Get-WmiObject Win32_OperatingSystem).OSArchitecture -eq '64-bit') {
    cmd /c start /wait $destPath\SSCERuntime_x64-ENU.msi /quiet /norestart
} Else {
    cmd /c start /wait $destPath\SSCERuntime_x86-ENU.msi /quiet /norestart
}

Write-Host "SQL Server Compact installed" -foregroundcolor Green

Write-Host "Run Parameters:" -ForegroundColor Cyan
Write-Host "  CreatePackages: $CreatePackages"
Write-Host "  RunTests: $RunTests"
Write-Host "  dotnet --version:" (dotnet --version)

$packageOutputFolder = "$PSScriptRoot\.nupkgs"
$projectsToBuild =
    'Dapper.Database'

$testsToRun =
    'Dapper.Database.Tests'

if ($PullRequestNumber) {
    Write-Host "Building for a pull request (#$PullRequestNumber), skipping packaging." -ForegroundColor Yellow
    $CreatePackages = $false
}

Write-Host "Restoring all projects..." -ForegroundColor "Magenta"
dotnet restore
Write-Host "Done restoring." -ForegroundColor "Green"

Write-Host "Building all projects..." -ForegroundColor "Magenta"
dotnet build -c Release --no-restore /p:CI=true
Write-Host "Done building." -ForegroundColor "Green"

if ($RunTests) {
    foreach ($project in $testsToRun) {
        Write-Host "Running tests: $project (all frameworks)" -ForegroundColor "Magenta"
        Push-Location ".\$project"

        dotnet test -c Release
        if ($LastExitCode -ne 0) {
            Write-Host "Error with tests, aborting build." -Foreground "Red"
            Pop-Location
            Exit 1
        }

        Write-Host "Tests passed!" -ForegroundColor "Green"
	    Pop-Location
    }
}

if ($CreatePackages) {
    mkdir -Force $packageOutputFolder | Out-Null
    Write-Host "Clearing existing $packageOutputFolder..." -NoNewline
    Get-ChildItem $packageOutputFolder | Remove-Item
    Write-Host "done." -ForegroundColor "Green"

    Write-Host "Building all packages" -ForegroundColor "Green"

    foreach ($project in $projectsToBuild) {
        Write-Host "Packing $project (dotnet pack)..." -ForegroundColor "Magenta"
        dotnet pack ".\$project\$project.csproj" --no-build -c Release /p:PackageOutputPath=$packageOutputFolder /p:NoPackageAnalysis=true /p:CI=true
        Write-Host ""
    }
}
Write-Host "Build Complete." -ForegroundColor "Green"
