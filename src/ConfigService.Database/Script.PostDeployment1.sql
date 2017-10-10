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


/* Customer */
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'65e51c54-21c5-41e8-8e22-21500379b275', N'Globex Corporation', N'Nothing says, “nonspecific international company serving the needs of consumers through service and synergy” like Globex.', CAST(N'2017-09-28 11:36:27.047' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'450c0a80-cbe5-4dbb-bd57-2d069da88959', N'Vehement Capital Partners', N'Vehement says intense, passionate, insistent; it’s a word for describing an argument or an artistic platform.', CAST(N'2017-09-28 11:38:03.423' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'5a8b5aad-303f-41ed-a3aa-6906c6d13767', N'Umbrella Corporation', N'In the Resident Evil Universe, there’s Umbrella Corporation.', CAST(N'2017-09-28 11:37:24.817' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'17e223c8-f381-42b2-aea2-72823afa98d4', N'Initech', N'A veteran namer could break this down upon first sight: “ini” – innovation, “tech” – technology. And then suddenly, Initech reveals itself as a redundant and meaningless moniker, despite sounding almost as powerful as Intel and as fundamental as tech support.', CAST(N'2017-09-28 11:37:07.200' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'c4fc61c3-3afb-4c64-994a-8a1601a6739f', N'Massive Dynamic', N'This is a name so good we wish we’d come up with it first.', CAST(N'2017-09-28 11:38:20.703' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'fc5ab70f-2b78-4806-a78a-b2c2fe7bfc10', N'Acme Corporation', N'This diabolical Looney Tunes mega-corp responsible for falling anvils and other humorous disasters has created some of our best memories of cartoon mischief.', CAST(N'2017-09-28 11:36:01.400' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'373df2c3-2328-4cf2-afef-dbfac08db885', N'Hooli', N'The HBO hit series Silicon Valley’s Hooli coyly smacks of the signature sound and feel we can’t quite put our fingers on: that goofy yet apt naming convention of mega-tech companies like Yahoo, Hulu, and Google.', CAST(N'2017-09-28 11:37:45.740' AS DateTime))
GO
INSERT [dbo].[Customers] ([Id], [Name], [Description], [CreatedDate]) VALUES (N'6549f610-8c36-43b7-b33a-f79a308cc902', N'Soylent Corp', N'Had Soylent Corp not inspired a real-life brand name of nutritional supplement and viral sensation, it wouldn’t have made this list.', CAST(N'2017-09-28 11:36:45.410' AS DateTime))
GO


/* SettingType */
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (0, NULL, N'Root', 1, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (1, 0, N'Email Security', 2, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (2, 0, N'Web Security', 3, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (3, 0, N'Password Reset', 4, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (4, 0, N'Cloud Application Security', 5, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (5, 0, N'Unified Security Service', 6, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
INSERT [dbo].[SettingTypes] ([Id], [ParentId], [Description], [SequenceNumber], [Enabled], [CreatedDate]) VALUES (6, 0, N'Multi-Factor Authentication', 7, 1, CAST(N'2017-09-28 11:48:01.023' AS DateTime))
GO
