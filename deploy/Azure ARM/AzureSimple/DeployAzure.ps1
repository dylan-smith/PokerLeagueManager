#Requires -Version 3.0

Set-StrictMode -Version 3
Import-Module Azure -ErrorAction SilentlyContinue

$ResourceGroupName = Read-Host "Resource Group Name (must be globally unique)"
$tfsBuildNumber = Read-Host "TFS Build Number (e.g. 20150604.3)"

Add-AzureAccount -verbose
Select-AzureSubscription -SubscriptionName "MSDN MPN"

$ResourceGroupLocation = "West US"
$TemplateFile = Join-Path $PSScriptRoot "AzureSimple.json"
$TemplateParams = @{tfsBuildNumber="$tfsBuildNumber"}

# Create or update the resource group using the specified template file and template parameters file
Switch-AzureMode AzureResourceManager
New-AzureResourceGroup -Name $ResourceGroupName `
                       -Location $ResourceGroupLocation `
                       -TemplateFile $TemplateFile `
                       -TemplateParameterObject $TemplateParams `
                       -Force -Verbose
