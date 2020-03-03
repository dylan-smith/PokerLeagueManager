﻿CREATE TABLE [DTO].[GetGamePlayersDto]
(
	[DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[GameId] UNIQUEIDENTIFIER NOT NULL, 
    [PlayerId] UNIQUEIDENTIFIER NOT NULL, 
    [PlayerName] VARCHAR(50) NOT NULL, 
    [PayIn] INT NULL, 
    [Winnings] INT NULL, 
    [Placing] INT NULL,
    [Rebuys] INT NULL
)
