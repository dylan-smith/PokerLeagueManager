﻿CREATE TABLE [dbo].[GetPlayerGamesDto]
(
    [DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [GameId] UNIQUEIDENTIFIER NOT NULL, 
    [GameDate] DATETIME NOT NULL, 
    [PlayerName] VARCHAR(MAX) NOT NULL, 
    [Placing] INT NOT NULL,
	[Winnings] INT NOT NULL,
	[PayIn] INT NOT NULL
)
