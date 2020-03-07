ALTER TABLE [DTO].[GetGamePlayersDto]
    ADD [Rebuys] [int] NULL
GO

UPDATE [DTO].[GetGamePlayersDto] SET Rebuys = 0
GO

ALTER TABLE [DTO].[GetGamePlayersDto]
    ALTER COLUMN [Rebuys] [int] NOT NULL
GO