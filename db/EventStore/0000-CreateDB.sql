CREATE TABLE [dbo].[AggregateLocks](
	[AggregateId] [uniqueidentifier] NOT NULL,
	[LockExpiry] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AggregateId] ASC
)
)
GO

CREATE TABLE [dbo].[Commands](
	[CommandId] [uniqueidentifier] NOT NULL,
	[CommandDateTime] [datetime] NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[CommandData] [varchar](max) NOT NULL,
	[ExceptionDetails] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CommandId] ASC
)
)
GO

CREATE TABLE [dbo].[Events](
	[EventId] [uniqueidentifier] NOT NULL,
	[EventTimestamp] [timestamp] NOT NULL,
	[EventDateTime] [datetime] NOT NULL,
	[CommandId] [uniqueidentifier] NOT NULL,
	[AggregateId] [uniqueidentifier] NOT NULL,
	[EventType] [varchar](max) NOT NULL,
	[EventData] [varchar](max) NOT NULL,
	[Published] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)
)
GO

CREATE TABLE [dbo].[Subscribers](
	[SubscriberId] [uniqueidentifier] NOT NULL,
	[SubscriberUrl] [varchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubscriberId] ASC
)
)
GO