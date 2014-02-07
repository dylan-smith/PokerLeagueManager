/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

DELETE FROM Subscribers

IF '$(PublishEnvironment)' = 'dev'
BEGIN
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:1766/Infrastructure/EventService.svc')
END

IF '$(PublishEnvironment)' = 'Local'
BEGIN
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:83/PokerLeagueManager.Queries.WCF/EventService.svc')
END

IF '$(PublishEnvironment)' = 'Build'
BEGIN
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:783/PokerLeagueManager.Queries.WCF/EventService.svc')
END