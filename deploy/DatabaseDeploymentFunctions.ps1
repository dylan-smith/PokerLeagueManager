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

    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword

	$Sql = "IF (DB_ID(N'$DatabaseName') IS NOT NULL) `n"
	$Sql += "BEGIN `n"
	$Sql += "  ALTER DATABASE [$DatabaseName] `n"
	$Sql += "  SET SINGLE_USER WITH ROLLBACK IMMEDIATE;`n"
	$Sql += "  DROP DATABASE [$DatabaseName];`n"
	$Sql += "END"

	Execute-NonQuery $Sql $Conn
    $Conn.Close()

	Write-Verbose "Dropping Database $DatabaseName...Completed"
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

function Create-Database
{
    param([string]$DatabaseServerName, 
          [string]$DatabaseName, 
          [string]$DatabaseLogin, 
          [string]$DatabasePassword)

    Write-Verbose "Creating database $DatabaseName..."

    if ([string]::IsNullOrWhiteSpace($DatabaseName)) {
	    Write-Error "Database Name must be provided"
    }

	$CreateDBScriptName = "0000-CreateDB.sql"
	$CreateDBPath = Join-Path $DatabaseUpgradeScriptsPath $CreateDBScriptName
	
	if ((Test-Path $CreateDBPath) -eq $false) {
		Write-Error "Could not find file: $CreateDBPath"
	}
    
	Write-Verbose "Running $CreateDBPath..."

    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
    $Server = Get-SmoServer $Conn
    
	$Sql = [IO.File]::ReadAllText($CreateDBPath)
	Execute-SQLCMD $Sql $Server

    $Conn.Close()

    Write-Verbose "Creating $ScriptsTable table..."
    $Conn = Get-SqlConnection -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName -DatabaseLogin $DatabaseLogin -DatabasePassword $DatabasePassword
	
	$Sql = "SELECT COUNT(*) FROM sys.tables WHERE name = '$ScriptsTable'"
	$TableCount = Execute-Scalar $Sql $Conn
	
	if ($TableCount -gt 0) {
		Write-Error "$CreateDBScriptName should not create the $ScriptsTable table"
	}

	$Sql = "CREATE TABLE [dbo].[$ScriptsTable] `n"
	$Sql += "( `n"
	$Sql += "  [ScriptName] VARCHAR(255) NOT NULL PRIMARY KEY, `n"
	$Sql += "  [DateRun] DATETIME NOT NULL `n"
	$Sql += ")"
	
	Execute-NonQuery $Sql $Conn
	Add-RowToScriptsTable $Conn $CreateDBScriptName

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
    
    $Sql = "SELECT * FROM $ScriptsTable"
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