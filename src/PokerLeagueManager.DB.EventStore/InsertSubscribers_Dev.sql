CREATE PROCEDURE [dbo].[InsertSubscribers_Dev]
AS
	
DELETE FROM Subscribers

INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:1766/Infrastructure/EventService.svc')
