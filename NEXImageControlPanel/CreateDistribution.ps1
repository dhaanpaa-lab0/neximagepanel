#!/usr/bin/env pwsh
# Output folder
$outputFolderPath = "./out"
$outputFolderExists = Test-Path $outputFolderPath -PathType Container
if (!$outputFolderExists) {
    New-Item -ItemType Directory -Path $outputFolderPath
}

# Dist folder
$distFolderPath = "./dist"
$distFolderExists = Test-Path $distFolderPath -PathType Container
if (!$distFolderExists) {
    New-Item -ItemType Directory -Path $distFolderPath
}


$dt = [datetime]::Now.ToString("MMddyy-hhmmsss")
Write-Host $dt
dotnet publish -o $distFolderPath

$productVersionHash = (Get-Item .\dist\NEXImageControlPanel.exe).VersionInfo.ProductVersion.Split(".") | Select-Object -Last 1
$productPackFile = "./out/NICPDist.$($productVersionHash).$dt.zip"
Get-ChildItem -Path $distFolderPath -Recurse |
  Compress-Archive -DestinationPath $productPackFile

