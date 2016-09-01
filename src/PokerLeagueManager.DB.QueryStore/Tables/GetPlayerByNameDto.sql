CREATE TABLE [dbo].[GetPlayerByNameDto]
(
	[DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[PlayerName] VARCHAR(MAX) NOT NULL, 
    [GameCount] INT NOT NULL, 
)
