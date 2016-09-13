"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.com" "PokerLeagueManager.sln" /Build Debug
del C:\PokerLeagueManager.UI.Wpf\* /S /Q
xcopy PokerLeagueManager.UI.Wpf\bin\Debug\* C:\PokerLeagueManager.UI.Wpf\
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\sqlpackage.exe" /SourceFile:"PokerLeagueManager.DB.EventStore\bin\Debug\PokerLeagueManager.DB.EventStore.dacpac" /Action:Publish /TargetServerName:localhost /TargetDatabaseName:PokerLeagueManager.DB.EventStore /p:CreateNewDatabase=true
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\130\sqlpackage.exe" /SourceFile:"PokerLeagueManager.DB.QueryStore\bin\Debug\PokerLeagueManager.DB.QueryStore.dacpac" /Action:Publish /TargetServerName:localhost /TargetDatabaseName:PokerLeagueManager.DB.QueryStore /p:CreateNewDatabase=true
PokerLeagueManager.Utilities.CreateEventSubscriber\bin\Debug\PokerLeagueManager.Utilities.CreateEventSubscriber.exe localhost http://localhost:1766/EventService.svc

REM "C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "PokerLeagueManager.UI.Wpf.CodedUITests\bin\Debug\PokerLeagueManager.UI.Wpf.CodedUITests.dll"

pause