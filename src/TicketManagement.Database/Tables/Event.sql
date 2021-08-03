CREATE TABLE [dbo].[Event]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[LayoutId] int NOT NULL, 
    [EventStart] DATETIME NOT NULL, 
    [EventEnd] DATETIME NOT NULL, 
    [Image] NVARCHAR(MAX) NULL,
)
