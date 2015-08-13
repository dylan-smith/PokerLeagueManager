Param(
	[string]$tfsBuildNumber
)

Import-Module "C:\Program Files (x86)\Microsoft SDKs\Azure\PowerShell\ServiceManagement\Azure\Azure.psd1"
Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
$ctx = New-AzureStorageContext -StorageAccountName dylanpokerstorage -StorageAccountKey efo1eHJJAouO4n/73Xc4EJRHBdu/r+n4iChmw61ThRMwQhJi1nztBXTxUpc5meTZD8OUwsqCx4I6BfJFnpLp3g==

$build_outputs = Join-Path $PSScriptRoot "BuildOutputs\"
$deploy_dir = Join-Path $PSScriptRoot "Deployment\"
mkdir $deploy_dir

### Common Functions ###

function Create-ServerDir([string]$EnvDir, [string]$ServerName){
	$ServerDir = Join-Path $EnvDir $ServerName
	mkdir $ServerDir | Out-Null

	copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$ServerDir\Deploy.ps1" | Out-Null
	copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$ServerDir\Deploy.cmd" | Out-Null

	return $ServerDir
}

function Copy-CommandsWCF([string]$TargetDir){
	Write-Host "TargetDir: $TargetDir"
	mkdir "$TargetDir\PokerLeagueManager.Commands.WCF"
	copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$TargetDir\PokerLeagueManager.Commands.WCF\"
}

function Copy-QueriesWCF([string]$TargetDir){
	mkdir "$TargetDir\PokerLeagueManager.Queries.WCF"
	copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$TargetDir\PokerLeagueManager.Queries.WCF\"
}

function Copy-DBEventStore([string]$TargetDir, [string]$EnvName){
	mkdir "$TargetDir\PokerLeagueManager.DB.EventStore"
	copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$TargetDir\PokerLeagueManager.DB.EventStore\"
	copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.EventStore.$EnvName.publish.xml" -dest "$TargetDir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"
}

function Copy-DBQueryStore([string]$TargetDir, [string]$EnvName){
	mkdir "$TargetDir\PokerLeagueManager.DB.QueryStore"
	copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$TargetDir\PokerLeagueManager.DB.QueryStore\"
	copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.QueryStore.$EnvName.publish.xml" -dest "$TargetDir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"
}

function Copy-UIWPF([string]$TargetDir){
	mkdir "$TargetDir\PokerLeagueManager.UI.WPF"
	copy -path "$build_outputs\log4net.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe.config" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
	copy -path "$build_outputs\System.Windows.Interactivity.dll" -dest "$TargetDir\PokerLeagueManager.UI.WPF\"
}

function Copy-UtilitiesProcessEvents([string]$TargetDir){
	mkdir "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents"
	copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Commands.Domain.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Common.Events.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
	copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe.config" -dest "$TargetDir\PokerLeagueManager.Utilities.ProcessEvents\"
}

### Local Environment ###

$EnvName = "Local"
$EnvDir = Join-Path $deploy_dir "$EnvName Environment"
mkdir $EnvDir
$local_env_dir = $EnvDir

$ServerName = "localhost"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Write-Host $ServerDir

Copy-CommandsWCF $ServerDir
Copy-QueriesWCF $ServerDir
Copy-DBEventStore $ServerDir $EnvName
Copy-DBQueryStore $ServerDir $EnvName
Copy-UIWPF $ServerDir
Copy-UtilitiesProcessEvents $ServerDir


### Build Environment ###

$EnvName = "Build"
$EnvDir = Join-Path $deploy_dir "$EnvName Environment"
mkdir $EnvDir
$build_env_dir = $EnvDir

$ServerName = "Build Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName

Copy-CommandsWCF $ServerDir
Copy-QueriesWCF $ServerDir
Copy-DBEventStore $ServerDir $EnvName
Copy-DBQueryStore $ServerDir $EnvName
Copy-UIWPF $ServerDir
Copy-UtilitiesProcessEvents $ServerDir


### AzureSimple Environment ###

$EnvName = "AzureSimple"
$EnvDir = Join-Path $deploy_dir "$EnvName Environment"
mkdir $EnvDir
$azuresimple_env_dir = $EnvDir

$ServerName = "Azure Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName

Copy-CommandsWCF $ServerDir
Copy-QueriesWCF $ServerDir
Copy-DBEventStore $ServerDir $EnvName
Copy-DBQueryStore $ServerDir $EnvName
Copy-UIWPF $ServerDir
Copy-UtilitiesProcessEvents $ServerDir


### AzureComplex Environment ###

$EnvName = "AzureComplex"
$EnvDir = Join-Path $deploy_dir "$EnvName Environment"
mkdir $EnvDir
$azurecomplex_env_dir = $EnvDir

$ServerName = "Command Web Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Copy-CommandsWCF $ServerDir

$ServerName = "Query Web Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Copy-QueriesWCF $ServerDir

$ServerName = "Event DB Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Copy-DBEventStore $ServerDir $EnvName
Copy-UtilitiesProcessEvents $ServerDir

$ServerName = "Query DB Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Copy-DBQueryStore $ServerDir $EnvName

$ServerName = "Client Server"
$ServerDir = Create-ServerDir $EnvDir $ServerName
Copy-UIWPF $ServerDir


############################

copy -path "$build_outputs\deploy\config\*" -dest "$deploy_dir\" -Force -Recurse

############################

$zipFileName = Join-Path $deploy_dir "$tfsBuildNumber-BUILD.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($build_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = Join-Path $deploy_dir "$tfsBuildNumber-LOCAL.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($local_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = Join-Path $deploy_dir "$tfsBuildNumber-AZURESIMPLE.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($azuresimple_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = Join-Path $deploy_dir "$tfsBuildNumber-AZURECOMPLEX.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($azurecomplex_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx