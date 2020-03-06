[Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.ConnectionInfo") | Out-Null
[Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.Smo') | Out-Null

function Execute-NonQuery
{
	param([string]$Sql, [System.Data.SqlClient.SqlConnection]$Conn)
	
	$Cmd = New-Object System.Data.SqlClient.SqlCommand
	$Cmd.Connection = $Conn
	$Cmd.CommandText = $Sql
	
	try {
		$Cmd.ExecuteNonQuery() | Out-Null
	}
	catch [System.Exception] {
		Write-Error $_.Exception.InnerException.Message
	}
}

function Execute-Scalar
{
	param([string]$Sql, [System.Data.SqlClient.SqlConnection]$Conn)
	
	$Cmd = New-Object System.Data.SqlClient.SqlCommand
	$Cmd.Connection = $Conn
	$Cmd.CommandText = $Sql
	
	try {
		Write-Output $Cmd.ExecuteScalar()
	}
	catch [System.Exception] {
		Write-Error $_.Exception.InnerException.Message
	}
}

function Get-DataTable
{
	param([string]$Sql, [System.Data.SqlClient.SqlConnection]$Conn)
	
    try {
		$Cmd = New-Object System.Data.SqlClient.SqlCommand
		$Cmd.Connection = $Conn
		$Cmd.CommandText = $Sql
	
		$Adapter = New-Object System.Data.SqlClient.SqlDataAdapter
		$Adapter.SelectCommand = $Cmd

		$Result = New-Object System.Data.DataTable
		$Adapter.Fill($Result) | Out-Null

		Write-Output -NoEnumerate $Result
	}
	catch [System.Exception] {
		Write-Error $_.Exception.InnerException.Message
	}
}

function Execute-SQLCMD
{
	param([string]$Sql, [Microsoft.SqlServer.Management.Smo.Server]$Server)
	
	try {
		$Server.ConnectionContext.ExecuteNonQuery($Sql) | Out-Null
	}
	catch [System.Exception] {
		Write-Error $_.Exception.InnerException.Message
	}
}

function Get-SqlConnection
{
    param([string]$DatabaseServerName,
          [string]$DatabaseName,
          [string]$DatabaseLogin,
          [string]$DatabasePassword)

    if ([string]::IsNullOrWhiteSpace($DatabaseServerName)) {
	    Write-Error "Database Server must be provided"
    }

    if ([string]::IsNullOrWhiteSpace($DatabaseName))
    {
        if ([string]::IsNullOrWhiteSpace($DatabaseLogin)) {
	        $ConnString = "Data Source=$DatabaseServerName;Integrated Security=True"
        }
        else {
	        $ConnString = "Server=$DatabaseServerName;User Id=$DatabaseLogin;Password=$DatabasePassword;"
        }
    } else {
        if ([string]::IsNullOrWhiteSpace($DatabaseLogin)) {
	        $ConnString = "Data Source=$DatabaseServerName;Initial Catalog=$DatabaseName;Integrated Security=True"
        }
        else {
    	    $ConnString = "Server=$DatabaseServerName;Database=$DatabaseName;User Id=$DatabaseLogin;Password=$DatabasePassword;"
        }
    }

    Write-Verbose "Using Connection String: $ConnString"

    $Conn = New-Object System.Data.SqlClient.SqlConnection
    $Conn.ConnectionString = $ConnString
    $Conn.Open()

    Write-Output $Conn
}

function Get-SmoServer
{
    param([System.Data.SqlClient.SqlConnection]$Conn)

    $ServerConnection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection($Conn)
    $Server = New-Object Microsoft.SqlServer.Management.Smo.Server($ServerConnection)

    Write-Output $Server
}