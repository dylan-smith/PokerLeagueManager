param(
    [string]$tfsBuildNumber,
    [string]$deployScript
)

$destinationPath = "C:\Deploy\"
$zipFileName = "$tfsBuildNumber.zip"
$scriptPath = $MyInvocation.MyCommand.Path
$sourcePath = Split-Path $scriptPath
$zipPath = Join-Path $sourcePath $zipFileName
$deployScriptPath = Join-Path $destinationPath $deployScript # C:\Deploy\Azure Environment\Azure Server\Deploy.ps1
$deployPath = Split-Path $deployScriptPath # C:\Deploy\Azure Environment\Azure Server\

New-Item -ItemType Directory -Force -Path $destinationPath -WarningAction SilentlyContinue

$shell = new-object -com shell.application
$shell.namespace($destinationPath).copyhere($shell.namespace($zipPath).items(), 1556) 

Import-Module WebAdministration
$iisAppPoolName = "PokerAppPool"
$iisAppPoolDotNetVersion = "v4.0"

#navigate to the app pools root
cd IIS:\AppPools\

#check if the app pool exists
if (!(Test-Path $iisAppPoolName -pathType container))
{
    #create the app pool
    $appPool = New-Item $iisAppPoolName
    #$appPool | Set-ItemProperty -Name "managedRuntimeVersion" -Value $iisAppPoolDotNetVersion
    $appPool.managedRuntimeVersion = $iisAppPoolDotNetVersion
    $appPool.processModel.username = "COMMANDSERVER\Dylan"
    $appPool.processModel.password = "P2ssw0rd"
    $appPool.processModel.identityType = 3
    $appPool | Set-Item
}

Set-ItemProperty "IIS:\Sites\Default Web Site" -Name applicationpool -Value "$iisAppPoolName"

$sqlInstallCommand = "C:\SQLInstall\setup.exe "
$sqlInstallCommand += "/QUIET "
$sqlInstallCommand += "/IACCEPTSQLSERVERLICENSETERMS "
$sqlInstallCommand += "/ACTION=CompleteImage "
$sqlInstallCommand += "/PID=748RB-X4T6B-MRM7V-RTVFF-CHC8H "
$sqlInstallCommand += "/ENU=True "
$sqlInstallCommand += "/HELP=FALSE "
$sqlInstallCommand += "/INDICATEPROGRESS=True "
$sqlInstallCommand += "/X86=False "
$sqlInstallCommand += "/INSTANCENAME=MSSQLSERVER "
$sqlInstallCommand += "/INSTANCEID=MSSQLSERVER "
$sqlInstallCommand += "/SQMREPORTING=False "
$sqlInstallCommand += "/RSINSTALLMODE=DefaultNativeMode "
$sqlInstallCommand += "/ERRORREPORTING=False "
$sqlInstallCommand += "/AGTSVCACCOUNT=`"NT SERVICE\SQLSERVERAGENT`" "
$sqlInstallCommand += "/AGTSVCSTARTUPTYPE=Manual "
$sqlInstallCommand += "/ISSVCSTARTUPTYPE=Automatic "
$sqlInstallCommand += "/ISSVCACCOUNT=`"NT Service\MsDtsServer110`" "
$sqlInstallCommand += "/ASSVCACCOUNT=`"NT Service\MSSQLServerOLAPService`" "
$sqlInstallCommand += "/ASSVCSTARTUPTYPE=Automatic "
$sqlInstallCommand += "/ASCOLLATION=Latin1_General_CI_AS "
$sqlInstallCommand += "/ASDATADIR=`"C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Data`" "
$sqlInstallCommand += "/ASLOGDIR=`"C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Log`" "
$sqlInstallCommand += "/ASBACKUPDIR=`"C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Backup`" "
$sqlInstallCommand += "/ASTEMPDIR=`"C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Temp`" "
$sqlInstallCommand += "/ASCONFIGDIR=`"C:\Program Files\Microsoft SQL Server\MSAS11.MSSQLSERVER\OLAP\Config`" "
$sqlInstallCommand += "/ASPROVIDERMSOLAP=1 "
$sqlInstallCommand += "/ASSYSADMINACCOUNTS=`"COMMANDSERVER\Dylan`" `"NT AUTHORITY\SYSTEM`" "
$sqlInstallCommand += "/ASSERVERMODE=MULTIDIMENSIONAL "
$sqlInstallCommand += "/SQLSVCSTARTUPTYPE=Automatic "
$sqlInstallCommand += "/FILESTREAMLEVEL=0 "
$sqlInstallCommand += "/ENABLERANU=False "
$sqlInstallCommand += "/SQLCOLLATION=SQL_Latin1_General_CP1_CI_AS "
$sqlInstallCommand += "/SQLSVCACCOUNT=`"NT Service\MSSQLSERVER`" "
$sqlInstallCommand += "/SQLSYSADMINACCOUNTS=`"COMMANDSERVER\Dylan`" `"NT AUTHORITY\SYSTEM`" "
$sqlInstallCommand += "/ADDCURRENTUSERASSQLADMIN=False "
$sqlInstallCommand += "/TCPENABLED=1 "
$sqlInstallCommand += "/NPENABLED=0 "
$sqlInstallCommand += "/BROWSERSVCSTARTUPTYPE=Disabled "
$sqlInstallCommand += "/RSSVCACCOUNT=`"NT Service\ReportServer`" "
$sqlInstallCommand += "/RSSVCSTARTUPTYPE=Automatic "
$sqlInstallCommand += "/FTSVCACCOUNT=`"NT Service\MSSQLFDLauncher`""

Invoke-Expression $sqlInstallCommand

cd $deployPath
powershell -ExecutionPolicy unrestricted -File $deployScriptPath