$ScriptsTable = 'ScriptsRun'

. .\SqlFunctions.ps1

function Get-ScriptsAlreadyRun
{
	param([System.Data.SqlClient.SqlConnection]$Conn)
	
	try {
		$Sql = "SELECT * FROM $ScriptsTable"
		$RunScripts = [System.Data.DataTable](Get-DataTable $Sql $Conn)
		$Result = @()

		foreach ($row in $RunScripts.Rows) {
			$Result += $row["ScriptName"].ToString()
		}

		Write-Output -NoEnumerate $Result
	}
	catch [System.Exception] {
		Write-Error $_.Exception.InnerException.Message
	}
}

function Add-RowToScriptsTable 
{
	param([System.Data.SqlClient.SqlConnection]$Conn, 
          [string]$ScriptName)

	Write-Verbose "Inserting into $ScriptsTable table: $ScriptName"

	$Sql = "INSERT INTO $ScriptsTable(ScriptName, DateRun) VALUES('$ScriptName', GETDATE())"
	Execute-NonQuery $Sql $Conn
}

function Test-Database
{
    param([string]$DatabaseServerName, 
          [string]$DatabaseName, 
          [string]$DatabaseLogin, 
          [string]$DatabasePassword)

    if ([string]::IsNullOrWhiteSpace($DatabaseName)) {
	    Write-Error "Database Name must be provided"
    }

    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword

    $Sql = "SELECT COUNT(*) FROM sys.databases WHERE name = '$DatabaseName'"
    $DatabaseCount = Execute-Scalar $Sql $Conn
    $Conn.Close()
    
    Write-Output ($DatabaseCount -gt 0)
}

function Get-DatabaseEdition
{
    param([string]$DatabaseServerName,
          [string]$DatabaseName,
          [string]$DatabaseLogin,
          [string]$DatabasePassword)

    $Sql = "SELECT DATABASEPROPERTYEX('$DatabaseName', 'EDITION')"
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword

    $DatabaseEdition = Execute-Scalar $Sql $Conn
    $Conn.Close()

    Write-Verbose "Database Edition: $DatabaseEdition"
    Write-Output $DatabaseEdition
}

function Get-DatabaseServiceObjective
{
    param([string]$DatabaseServerName,
          [string]$DatabaseName,
          [string]$DatabaseLogin,
          [string]$DatabasePassword)

    $Sql = "SELECT DATABASEPROPERTYEX('$DatabaseName', 'ServiceObjective')"
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword

    $DatabaseServiceObjective = Execute-Scalar $Sql $Conn
    $Conn.Close()

    Write-Verbose "Database Service Objective: $DatabaseServiceObjective"
    Write-Output $DatabaseServiceObjective
}

function Drop-Database
{
    param([string]$DatabaseServerName, 
          [string]$DatabaseName, 
          [string]$DatabaseLogin, 
          [string]$DatabasePassword)

    Write-Verbose "Dropping Database $DatabaseName..."

    if ([string]::IsNullOrWhiteSpace($DatabaseName)) {
	    Write-Error "Database Name must be provided"
    }

    if (Test-Database -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword)
    {
        $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
        $Sql = "DROP DATABASE [$DatabaseName]"
        Execute-NonQuery $Sql $Conn
        $Conn.Close()

        while (Test-Database -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword)
        {
            Write-Verbose "Waiting for DB drop to complete..."
            sleep -Seconds 10
            $SleepCount++

            if ($SleepCount -gt 10)
            {
                Write-Error "100 seconds elapsed, DB still exists"
            }
        }
    }
    
	Write-Verbose "Dropping Database $DatabaseName...Completed"
}

function Create-Database
{
    param([string]$DatabaseServerName, 
          [string]$DatabaseName, 
          [string]$DatabaseLogin, 
          [string]$DatabasePassword,
          [string]$DatabaseEdition,
          [string]$DatabaseServiceObjective)

    Write-Verbose "Creating database $DatabaseName..."

    if ([string]::IsNullOrWhiteSpace($DatabaseName)) {
	    Write-Error "Database Name must be provided"
    }

    if ($DatabaseServerName.ToLower().Contains("database.windows.net"))
    {
        $Sql = "CREATE DATABASE [$DatabaseName] ( EDITION = '$DatabaseEdition', SERVICE_OBJECTIVE = '$DatabaseServiceObjective' )"
    }
    else
    {
        $Sql = "CREATE DATABASE [$DatabaseName]"
    }
    
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
    Execute-NonQuery $Sql $Conn
    $Conn.Close()

    Write-Verbose "Creating $ScriptsTable table..."
    
	$Sql = "CREATE TABLE [dbo].[$ScriptsTable] `n"
	$Sql += "( `n"
	$Sql += "  [ScriptName] VARCHAR(255) NOT NULL PRIMARY KEY, `n"
	$Sql += "  [DateRun] DATETIME NOT NULL `n"
	$Sql += ")"
	
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
	Execute-NonQuery $Sql $Conn
    $Conn.Close()

    Write-Verbose "Creating database $DatabaseName completed!"
}

function Upgrade-Database
{
    param([string]$DatabaseUpgradeScriptsPath, 
          [string]$DatabaseServerName, 
          [string]$DatabaseName, 
          [string]$DatabaseLogin, 
          [string]$DatabasePassword)

    Write-Verbose "Upgrading database $DatabaseName..."

    if ([string]::IsNullOrWhiteSpace($DatabaseName)) {
	    Write-Error "Database Name must be provided"
    }

    if ((Test-Path $DatabaseUpgradeScriptsPath) -eq $false) {
	    Write-Error "Path does not exist: $DatabaseUpgradeScriptsPath"
    }

    Write-Verbose "Looking in $DatabaseUpgradeScriptsPath for upgrade scripts..."
    $UpgradeScripts = @((Get-ChildItem -Path $DatabaseUpgradeScriptsPath -Filter "*.sql").Name | Sort-Object)
    Write-Verbose "Total Scripts: $($UpgradeScripts.length)"

    Write-Verbose "Looking in $ScriptsTable for scripts already run..."
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
    
    $Sql = "SELECT COUNT(*) FROM sys.tables WHERE name = '$ScriptsTable'"
    $TableCount = Execute-Scalar $Sql $Conn
    	
    if ($TableCount -ne 1) {
    	Write-Error "$ScriptsTable table does not exist in $DatabaseName"
    }
    
    $Sql = "SELECT * FROM [$ScriptsTable]"
    $ScriptsAlreadyRun = Get-ScriptsAlreadyRun $Conn
    
    $ScriptsToRun = @($UpgradeScripts | Where-Object {$ScriptsAlreadyRun -NotContains $_})
    
    Write-Verbose "Scripts Already Run: $($ScriptsAlreadyRun.length)"
    Write-Verbose "Running $($ScriptsToRun.Length) scripts..."

    $Server = Get-SmoServer $Conn
    
    foreach ($Script in $ScriptsToRun)
    {
        $ScriptPath = Join-Path $DatabaseUpgradeScriptsPath $Script
    
        Write-Verbose "Running $ScriptPath..."
    
        $Sql = [IO.File]::ReadAllText($ScriptPath)
    	Execute-SQLCMD $Sql $Server
    
        Add-RowToScriptsTable $Conn $Script
    }
    
    $Conn.Close()

    Write-Verbose "Upgrading database $DatabaseName completed!"
}