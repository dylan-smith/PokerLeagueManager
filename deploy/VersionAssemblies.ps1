[CmdletBinding()]
param(
    [string]$MajorVersion,
    [string]$MinorVersion,
    [string]$BuildNumber,
    [string]$SourcesDir
)

Write-Output "MajorVersion: $MajorVersion"
Write-Output "MinorVersion: $MinorVersion"
Write-Output "BuildNumber: $BuildNumber"
Write-Output "SourcesDir: $SourcesDir"

$Year = Get-Date -Format yy
$Month = Get-Date -Format MM
$Day = Get-Date -Format dd

$DotPosition = $BuildNumber.IndexOf(".")
$Revision = $BuildNumber.Substring($DotPosition + 1)
$Revision = $Revision.PadLeft(3, "0")

$NewVersion = "$MajorVersion.$MinorVersion.$Year$Month.$Day$Revision"

Write-Output "New Version: $NewVersion"

$VersionRegex = "\d+\.\d+\.\d+\.\d+"

# Apply the version to the assembly property files
$files = gci $SourcesDir -recurse | 
	?{ $_.PSIsContainer } | 
	foreach { gci -Path $_.FullName -Recurse -include *AssemblyInfo.* }
if($files)
{
	Write-Verbose "Will apply $NewVersion to $($files.count) files."
	
	foreach ($file in $files) {
		$filecontent = Get-Content($file)
		attrib $file -r
		$filecontent -replace $VersionRegex, $NewVersion | Out-File -Encoding "UTF8BOM" $file
		Write-Verbose "$file - version applied"
	}
}
else
{
	Write-Warning "Found no files."
}