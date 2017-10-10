CREATE TABLE [dbo].[Settings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] UNIQUEIDENTIFIER NOT NULL, 
    [SettingTypeId] INT NOT NULL, 
    [SettingValue] NVARCHAR(MAX) NULL
)
