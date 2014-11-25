CREATE TABLE [dbo].[BuyinDto]
(
	[DtoId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [GetGameResultsDto_DtoId] UNIQUEIDENTIFIER NOT NULL, 
    [BuyinAmount] INT NOT NULL, 
    CONSTRAINT [FK_BuyinDto_GetGameResultsDto] FOREIGN KEY ([GetGameResultsDto_DtoId]) REFERENCES [GetGameResultsDto]([DtoId])
)
