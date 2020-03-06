CREATE DATABASE [QueryStore]
GO

USE [QueryStore]
GO

CREATE SCHEMA [DTO]
GO

CREATE SCHEMA [Lookups]
GO

CREATE TABLE [dbo].[EventsProcessed](
	[EventId] [uniqueidentifier] NOT NULL,
	[ProcessedDateTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)
)
GO

CREATE TABLE [DTO].[GameLookupDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[GameDate] [datetime] NOT NULL,
	[Completed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GamePlayersLookupDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GetGameCountByDateDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[GameYear] [int] NOT NULL,
	[GameMonth] [int] NOT NULL,
	[GameDay] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GetGamePlayersDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[PlayerName] [varchar](50) NOT NULL,
	[PayIn] [int] NULL,
	[Winnings] [int] NULL,
	[Placing] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GetGamesListDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[GameId] [uniqueidentifier] NOT NULL,
	[GameDate] [datetime] NOT NULL,
	[Winner] [varchar](50) NULL,
	[Winnings] [int] NULL,
	[Completed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GetPlayerCountByNameDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[PlayerName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[GetPlayersDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[PlayerName] [varchar](50) NOT NULL,
	[GamesPlayed] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO

CREATE TABLE [DTO].[PlayerLookupDto](
	[DtoId] [uniqueidentifier] NOT NULL,
	[PlayerId] [uniqueidentifier] NOT NULL,
	[PlayerName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DtoId] ASC
)
)
GO