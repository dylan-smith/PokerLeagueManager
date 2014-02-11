﻿CREATE TABLE [dbo].[GetGameCountByDate]
(
	[DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[GameId] UNIQUEIDENTIFIER NOT NULL, 
    [GameYear] INT NOT NULL, 
    [GameMonth] INT NOT NULL, 
    [GameDay] INT NOT NULL
)
