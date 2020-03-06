[CmdletBinding()]
Param(
	[string]$DatabaseUpgradeScriptsPath,	
	[switch]$DropDatabase,
	[string]$DatabaseServerName,
	[string]$DatabaseName,
	[string]$DatabaseLogin,
	[string]$DatabasePassword
)

Write-Verbose "Database Upgrade Scripts Path: $DatabaseUpgradeScriptsPath"
Write-Verbose "Database Server: $DatabaseServerName"
Write-Verbose "Database Name: $DatabaseName"
Write-Verbose "Database Login: $DatabaseLogin"
Write-Verbose "Database Password: $DatabasePassword"
Write-Verbose "Drop Database: $DropDatabase"

$ErrorActionPreference = "Stop"

. .\DatabaseDeploymentFunctions.ps1

if ([System.IO.Path]::IsPathRooted($DatabaseUpgradeScriptsPath) -eq $false) {
	$DatabaseUpgradeScriptsPath = Join-Path $PSScriptRoot $DatabaseUpgradeScriptsPath
}

if ($DropDatabase) {
    Drop-Database $DatabaseServerName $DatabaseName $DatabaseLogin $DatabasePassword
}

if ((Test-Database $DatabaseServerName $DatabaseName $DatabaseLogin $DatabasePassword) -eq $false)
{
    Write-Verbose "Database $DatabaseName does not exist, creating database..."
    Create-Database $DatabaseServerName $DatabaseName $DatabaseLogin $DatabasePassword
}

Upgrade-Database $DatabaseUpgradeScriptsPath $DatabaseServerName $DatabaseName $DatabaseLogin $DatabasePassword