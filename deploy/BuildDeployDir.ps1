Param(
	[string]$tfsBuildNumber
)

Import-Module "C:\Program Files (x86)\Microsoft SDKs\Azure\PowerShell\ServiceManagement\Azure\Azure.psd1"
Add-Type -Assembly System.IO.Compression.FileSystem
$compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
$ctx = New-AzureStorageContext -StorageAccountName dylanpokerstorage -StorageAccountKey efo1eHJJAouO4n/73Xc4EJRHBdu/r+n4iChmw61ThRMwQhJi1nztBXTxUpc5meTZD8OUwsqCx4I6BfJFnpLp3g==

$build_outputs = "BuildOutputs"
$deploy_dir = "Deployment"
mkdir $deploy_dir

### Local Environment ###

$local_env_dir = "$deploy_dir\Local Environment"
mkdir "$local_env_dir"

$local_server_dir = "$local_env_dir\localhost"
mkdir "$local_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$local_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$local_server_dir\Deploy.cmd"

mkdir "$local_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$local_server_dir\PokerLeagueManager.Commands.WCF\"

mkdir "$local_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$local_server_dir\PokerLeagueManager.Queries.WCF\"

mkdir "$local_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$local_server_dir\PokerLeagueManager.DB.EventStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.EventStore.localhost.publish.xml" -dest "$local_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

mkdir "$local_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$local_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.QueryStore.localhost.publish.xml" -dest "$local_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

mkdir "$local_server_dir\PokerLeagueManager.UI.WPF"
copy -path "$build_outputs\log4net.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe.config" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\System.Windows.Interactivity.dll" -dest "$local_server_dir\PokerLeagueManager.UI.WPF\"

mkdir "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Commands.Domain.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Events.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe.config" -dest "$local_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"


### Build Environment ###

$build_env_dir = "$deploy_dir\Build Environment"
mkdir "$build_env_dir"

$build_server_dir = "$build_env_dir\Build Server"
mkdir "$build_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$build_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$build_server_dir\Deploy.cmd"

mkdir "$build_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$build_server_dir\PokerLeagueManager.Commands.WCF\"

mkdir "$build_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$build_server_dir\PokerLeagueManager.Queries.WCF\"

mkdir "$build_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$build_server_dir\PokerLeagueManager.DB.EventStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.EventStore.BUILD.publish.xml" -dest "$build_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

mkdir "$build_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$build_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.QueryStore.BUILD.publish.xml" -dest "$build_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

mkdir "$build_server_dir\PokerLeagueManager.UI.WPF"
copy -path "$build_outputs\log4net.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe.config" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\System.Windows.Interactivity.dll" -dest "$build_server_dir\PokerLeagueManager.UI.WPF\"

mkdir "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Commands.Domain.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Events.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe.config" -dest "$build_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"

### AzureSimple Environment ###

$azuresimple_env_dir = "$deploy_dir\AzureSimple Environment"
mkdir "$azuresimple_env_dir"

$azure_server_dir = "$azuresimple_env_dir\Azure Server"
mkdir "$azure_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$azure_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$azure_server_dir\Deploy.cmd"

mkdir "$azure_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$azure_server_dir\PokerLeagueManager.Commands.WCF\"

mkdir "$azure_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$azure_server_dir\PokerLeagueManager.Queries.WCF\"

mkdir "$azure_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$azure_server_dir\PokerLeagueManager.DB.EventStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.EventStore.AZURESIMPLE.publish.xml" -dest "$azure_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

mkdir "$azure_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$azure_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.QueryStore.AZURESIMPLE.publish.xml" -dest "$azure_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

mkdir "$azure_server_dir\PokerLeagueManager.UI.WPF"
copy -path "$build_outputs\log4net.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe.config" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\System.Windows.Interactivity.dll" -dest "$azure_server_dir\PokerLeagueManager.UI.WPF\"

mkdir "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Commands.Domain.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Events.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe.config" -dest "$azure_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"

### AzureComplex Environment ###

$azurecomplex_env_dir = "$deploy_dir\AzureComplex Environment"
mkdir "$build_env_dir"

$command_web_server_dir = "$build_env_dir\Command Web Server"
mkdir "$command_web_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$command_web_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$command_web_server_dir\Deploy.cmd"

mkdir "$command_web_server_dir\PokerLeagueManager.Commands.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Commands.WCF_Package\*" -dest "$command_web_server_dir\PokerLeagueManager.Commands.WCF\"

$query_web_server_dir = "$build_env_dir\Query Web Server"
mkdir "$query_web_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$query_web_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$query_web_server_dir\Deploy.cmd"

mkdir "$query_web_server_dir\PokerLeagueManager.Queries.WCF"
copy -path "$build_outputs\_PublishedWebsites\PokerLeagueManager.Queries.WCF_Package\*" -dest "$query_web_server_dir\PokerLeagueManager.Queries.WCF\"

$event_db_server_dir = "$build_env_dir\Event DB Server"
mkdir "$event_db_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$event_db_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$event_db_server_dir\Deploy.cmd"

mkdir "$event_db_server_dir\PokerLeagueManager.DB.EventStore"
copy -path "$build_outputs\PokerLeagueManager.DB.EventStore.dacpac" -dest "$event_db_server_dir\PokerLeagueManager.DB.EventStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.EventStore.AZURECOMPLEX.publish.xml" -dest "$event_db_server_dir\PokerLeagueManager.DB.EventStore\PokerLeagueManager.DB.EventStore.publish.xml"

$query_db_server_dir = "$build_env_dir\Query DB Server"
mkdir "$query_db_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$query_db_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$query_db_server_dir\Deploy.cmd"

mkdir "$build_server_dir\PokerLeagueManager.DB.QueryStore"
copy -path "$build_outputs\PokerLeagueManager.DB.QueryStore.dacpac" -dest "$query_db_server_dir\PokerLeagueManager.DB.QueryStore\"
copy -path "$build_outputs\Publish Profiles\PokerLeagueManager.DB.QueryStore.AZURECOMPLEX.publish.xml" -dest "$query_db_server_dir\PokerLeagueManager.DB.QueryStore\PokerLeagueManager.DB.QueryStore.publish.xml"

$client_server_dir = "$build_env_dir\Client Server"
mkdir "$client_server_dir"
copy -path "$build_outputs\Deploy\Deploy.ps1" -dest "$client_server_dir\Deploy.ps1"
copy -path "$build_outputs\Deploy\Deploy.cmd" -dest "$client_server_dir\Deploy.cmd"

mkdir "$client_server_dir\PokerLeagueManager.UI.WPF"
copy -path "$build_outputs\log4net.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\PokerLeagueManager.UI.WPF.exe.config" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"
copy -path "$build_outputs\System.Windows.Interactivity.dll" -dest "$client_server_dir\PokerLeagueManager.UI.WPF\"

mkdir "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents"
copy -path "$build_outputs\Microsoft.Practices.ServiceLocation.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\Microsoft.Practices.Unity.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Commands.Domain.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Commands.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.DTO.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Events.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Common.Utilities.dll" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"
copy -path "$build_outputs\PokerLeagueManager.Utilities.ProcessEvents.exe.config" -dest "$client_server_dir\PokerLeagueManager.Utilities.ProcessEvents\"

############################

copy -path "$build_outputs\deploy\config\*" -dest "$deploy_dir\" -Force -Recurse

############################

$zipFileName = "$deploy_dir\$tfsBuildNumber-BUILD.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($build_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = "$deploy_dir\$tfsBuildNumber-LOCAL.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($local_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = "$deploy_dir\$tfsBuildNumber-AZURESIMPLE.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($azuresimple_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx

############################

$zipFileName = "$deploy_dir\$tfsBuildNumber-AZURECOMPLEX.zip"

[System.IO.Compression.ZipFile]::CreateFromDirectory($azurecomplex_env_dir,$zipFileName,$compressionLevel,$true)
Set-AzureStorageBlobContent -File "$zipFileName" -Container "builds" -Context $ctx