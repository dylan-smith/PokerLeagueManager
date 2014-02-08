CREATE TABLE [dbo].[GetGameCountByDate]
(
	[GameId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [GameYear] INT NOT NULL, 
    [GameMonth] INT NOT NULL, 
    [GameDay] INT NOT NULL
)
