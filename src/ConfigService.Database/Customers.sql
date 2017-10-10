CREATE TABLE [dbo].[Customers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [Name] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Enabled] BIT NOT NULL DEFAULT 1,
	[CreatedDate] DATETIME NULL DEFAULT getdate()
)
