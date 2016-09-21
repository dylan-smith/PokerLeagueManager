param (
    [string]$queryServiceUrl,
    [string]$commandServiceUrl,
    [string]$configPath
)

Write-Output "queryServiceUrl: $queryServiceUrl"
Write-Output "commandServiceUrl: $commandServiceUrl"
Write-Output "configPath: $configPath"

$config = [xml](Get-Content $configPath)

$querySetting = $config.configuration.appSettings.add | where {$_.Key -eq 'QueryServiceUrl'}
$commandSetting = $config.configuration.appSettings.add | where {$_.Key -eq 'CommandServiceUrl'}

$querySetting.value = $queryServiceUrl
$commandSetting.value = $commandServiceUrl

$config.Save($configPath)