"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" "PokerLeagueManager.sln" /Build Debug
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\sqlpackage.exe" /SourceFile:"PokerLeagueManager.DB.EventStore\bin\Debug\PokerLeagueManager.DB.EventStore.dacpac" /Action:Publish /TargetServerName:localhost /TargetDatabaseName:PokerLeagueManager.DB.EventStore /p:CreateNewDatabase=true
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\sqlpackage.exe" /SourceFile:"PokerLeagueManager.DB.QueryStore\bin\Debug\PokerLeagueManager.DB.QueryStore.dacpac" /Action:Publish /TargetServerName:localhost /TargetDatabaseName:PokerLeagueManager.DB.QueryStore /p:CreateNewDatabase=true
PokerLeagueManager.Utilities\bin\Debug\PokerLeagueManager.Utilities.exe CreateEventSubscriber localhost PokerLeagueManager.DB.EventStore http://localhost:13831
PokerLeagueManager.Utilities\bin\Debug\PokerLeagueManager.Utilities.exe GenerateSampleData http://localhost:4224 400

pause