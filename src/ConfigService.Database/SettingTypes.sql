CREATE TABLE [dbo].[SettingTypes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [ParentId] INT NULL,
	[Description] NVARCHAR(50) NOT NULL, 
    [SequenceNumber] INT NULL, 
    [Enabled] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate()    
)
