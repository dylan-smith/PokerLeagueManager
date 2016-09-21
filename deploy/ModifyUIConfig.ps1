param (
    [string]$queryServiceUrl,
    [string]$commandServiceUrl,
    [string]$configPath
)

$config = [xml](Get-Content $configPath)

$querySetting = $config.configuration.appSettings.add | where {$_.Key -eq 'QueryServiceUrl'}
$commandSetting = $config.configuration.appSettings.add | where {$_.Key -eq 'CommandServiceUrl'}

$querySetting.value = $queryServiceUrl
$commandSetting.value = $commandServiceUrl

$config.Save($configPath)