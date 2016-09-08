param (
    [string]$accountName,
    [string]$teamProject,
    [string]$buildDefinitionName,
    [string]$targetFolder
)

Add-Type -AssemblyName System.Web
Add-Type -assembly “system.io.compression.filesystem”

$headers = @{Authorization = "Bearer $env:SYSTEM_ACCESSTOKEN"}

$accountName = [System.Web.HttpUtility]::UrlPathEncode($accountName)
$teamProject = [System.Web.HttpUtility]::UrlPathEncode($teamProject)
$restUrl = "https://$accountName.visualstudio.com/$teamProject/_apis/build/definitions?api-version=2.0"

Write-Output "Executing REST API: $restUrl"
$buildDefinitions = Invoke-RestMethod $restUrl -Headers $headers

$targetDefinition = $buildDefinitions.value | Where-Object {$_.name.ToLower() -eq $buildDefinitionName.ToLower()}
$buildDefinitionId = $targetDefinition.Id

$restUrl = "https://$accountName.visualstudio.com/$teamProject/_apis/build/builds?definitions=$buildDefinitionId&resultFilter=succeeded&statusFilter=completed&`$top=1&api-version=2.0"
Write-Output "Executing REST API: $restUrl"
$build = Invoke-RestMethod $restUrl -Headers $headers

$buildId = $build.value.id

$restUrl = "https://$accountName.visualstudio.com/$teamProject/_apis/build/builds/$buildId/artifacts?api-version=2.0"
Write-Output "Executing REST API: $restUrl"
$buildArtifacts = Invoke-RestMethod $restUrl -Headers $headers

$downloadUrl = $buildArtifacts.value.resource.downloadUrl
$zipPath = Join-Path $targetFolder "drop.zip"

Write-Output "Downloading Build Artifacts $downloadUrl..."
Invoke-WebRequest -Uri $downloadUrl -OutFile $zipPath -Headers $headers

[io.compression.zipfile]::ExtractToDirectory($zipPath, $targetFolder)

Remove-Item $zipPath