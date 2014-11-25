/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [EFSpike.DB]
GO
INSERT [dbo].[GetGameResultsDto] ([DtoId], [GameId], [GameDate]) VALUES (N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'43ce51cf-8564-484f-b9c8-d77387115018', CAST(0x0000A2C300000000 AS DateTime))
GO
INSERT [dbo].[GetGameResultsDto] ([DtoId], [GameId], [GameDate]) VALUES (N'51087e1e-2209-4ba4-b714-3cc4ddf5d7e4', N'392f7ce2-c52a-4280-9faf-672060cb675e', CAST(0x0000A34600000000 AS DateTime))
GO
INSERT [dbo].[GetGameResultsDto] ([DtoId], [GameId], [GameDate]) VALUES (N'cbe3d6fb-e4b6-4e3d-a581-51f104c1831c', N'e8309b22-8777-41b4-b2d4-9ea2ac6d0f9f', CAST(0x0000A34700000000 AS DateTime))
GO
INSERT [dbo].[GetGameResultsDto] ([DtoId], [GameId], [GameDate]) VALUES (N'24b868b9-96c6-449f-b664-64146152024d', N'0bccc477-8fe7-4aa9-a6d1-1941ca898b32', CAST(0x0000A2CA00000000 AS DateTime))
GO
INSERT [dbo].[GetGameResultsDto] ([DtoId], [GameId], [GameDate]) VALUES (N'bf6d134b-75ec-4e0d-b26d-cc978300523b', N'b01fe76b-ec27-44c6-963c-ca0779132cd7', CAST(0x0000A2D100000000 AS DateTime))
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'092ef582-46d9-4aa5-a79c-0a7a28730e27', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'Dylan Smith', 1, 7272)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'dc4821b7-f513-4bc7-887d-0aea947c5f11', N'24b868b9-96c6-449f-b664-64146152024d', N'Tim Saunders', 5, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'f31e58d9-75fc-465b-8b30-278c89953eef', N'24b868b9-96c6-449f-b664-64146152024d', N'Ryan Fritsch', 4, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'17dcef05-595c-493d-b803-4aabf058cb0c', N'24b868b9-96c6-449f-b664-64146152024d', N'Dylan Smith', 2, 50)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'6a34af19-7d0d-43e2-8c91-5112fb0d66b4', N'bf6d134b-75ec-4e0d-b26d-cc978300523b', N'GW Stein', 2, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'62672627-0839-4b98-8af4-5441644bc987', N'24b868b9-96c6-449f-b664-64146152024d', N'Shane Wilkins', 6, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'5d67559c-d0f2-42a4-b434-5cc0e92e8572', N'bf6d134b-75ec-4e0d-b26d-cc978300523b', N'Dylan Smith', 1, 130)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'462f1f3c-6452-419e-9588-6cc4158dc17c', N'cbe3d6fb-e4b6-4e3d-a581-51f104c1831c', N'Ryan', 2, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'34805ad7-953d-4303-ab9b-6ed538875141', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'Grant Hirose', 4, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'59a4975b-4d30-4d57-aa3f-7cdfce0dc443', N'51087e1e-2209-4ba4-b714-3cc4ddf5d7e4', N'George', 1, 100)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'0387e8fd-280f-48ff-8c30-a96ab7ab842f', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'Ryan Fritsch', 3, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'6b6db5e2-e5c6-47ac-9a84-c22ac7306dbd', N'51087e1e-2209-4ba4-b714-3cc4ddf5d7e4', N'Dave', 2, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'6c191d70-741d-4350-9d39-c9cbbbc01388', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'GW Stein', 5, 0)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'aac37f15-0de3-4882-bbca-df414100f307', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', N'Shane Wilkins', 2, 20)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'cce5422c-d284-492e-ba0f-ed0d60b34ef3', N'24b868b9-96c6-449f-b664-64146152024d', N'Colin Hickson', 3, 20)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'b284af07-142c-47f0-9772-f64fabf8e911', N'cbe3d6fb-e4b6-4e3d-a581-51f104c1831c', N'Dylan', 1, 1000)
GO
INSERT [dbo].[PlayerDto] ([DtoId], [GetGameResultsDto_DtoId], [PlayerName], [Placing], [Winnings]) VALUES (N'772b8482-43e6-4737-b66a-fddf3b363c3e', N'24b868b9-96c6-449f-b664-64146152024d', N'GW Stein', 1, 240)
GO
INSERT [dbo].[BuyinDto] ([DtoId], [GetGameResultsDto_DtoId], [BuyinAmount]) VALUES (N'724c85ba-8291-4289-9bd6-06ea7431259d', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', 120)
GO
INSERT [dbo].[BuyinDto] ([DtoId], [GetGameResultsDto_DtoId], [BuyinAmount]) VALUES (N'cd323355-34a9-4dd4-8414-9a7659ef2df3', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', 90)
GO
INSERT [dbo].[BuyinDto] ([DtoId], [GetGameResultsDto_DtoId], [BuyinAmount]) VALUES (N'0634c281-dac3-4a90-bcd6-d74de722524b', N'049e86c7-1f6c-4b02-b5f3-17b54f988ddc', 275)
GO
INSERT [dbo].[BuyinDto] ([DtoId], [GetGameResultsDto_DtoId], [BuyinAmount]) VALUES (N'0ba2323c-848c-4a4b-bb7a-98dc04486a17', N'51087e1e-2209-4ba4-b714-3cc4ddf5d7e4', 30)
GO
INSERT [dbo].[BuyinDto] ([DtoId], [GetGameResultsDto_DtoId], [BuyinAmount]) VALUES (N'334f2f4b-eb8f-47d0-af2d-a00b7e77bd5e', N'51087e1e-2209-4ba4-b714-3cc4ddf5d7e4', 10)
GO