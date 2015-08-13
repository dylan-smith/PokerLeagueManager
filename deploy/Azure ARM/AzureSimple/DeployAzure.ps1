#Requires -Version 3.0

Param(
  [string] [Parameter(Mandatory=$true)]$ResourceGroupName
)

Set-StrictMode -Version 3
Import-Module Azure -ErrorAction SilentlyContinue

Add-AzureAccount -verbose
Select-AzureSubscription -SubscriptionName "MSDN MPN"

$ResourceGroupLocation = "West US"
$TemplateFile = Join-Path $PSScriptRoot "AzureSimple.json"

# Create or update the resource group using the specified template file and template parameters file
Switch-AzureMode AzureResourceManager
New-AzureResourceGroup -Name $ResourceGroupName `
                       -Location $ResourceGroupLocation `
                       -TemplateFile $TemplateFile `
                       -Force -Verbose
