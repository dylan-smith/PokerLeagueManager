﻿CREATE TABLE [dbo].[LookupGamePlayersDto]
(
	[DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[GameId] UNIQUEIDENTIFIER NOT NULL,
	[PlayerName] VARCHAR(MAX) NOT NULL, 
    [Placing] INT NOT NULL, 
    [Winnings] INT NOT NULL, 
    [PayIn] INT NOT NULL, 
)
