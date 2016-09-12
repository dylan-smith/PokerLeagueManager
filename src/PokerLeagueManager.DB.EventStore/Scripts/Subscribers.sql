DELETE FROM Subscribers

INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:1766/Infrastructure/EventService.svc')
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:1766/Infrastructure/EventService.svc')
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost:783/PokerLeagueManager.Queries.WCF/Infrastructure/EventService.svc')
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://localhost/PokerLeagueManager.Queries.WCF/Infrastructure/EventService.svc')
INSERT INTO Subscribers(SubscriberId, SubscriberUrl) VALUES(newid(), 'http://QueryWeb/PokerLeagueManager.Queries.WCF/Infrastructure/EventService.svc')
