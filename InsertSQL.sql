USE [VHSMovieRentalDB]
GO
SET IDENTITY_INSERT [dbo].[MovieRentalTerm] ON 

INSERT [dbo].[MovieRentalTerm] ([MovieRentalTermID], [RentalDays], [LateReturnCharge]) VALUES (1, 2, CAST(0.99 AS Decimal(18, 2)))
INSERT [dbo].[MovieRentalTerm] ([MovieRentalTermID], [RentalDays], [LateReturnCharge]) VALUES (2, 5, CAST(1.25 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[MovieRentalTerm] OFF
SET IDENTITY_INSERT [dbo].[TransactionType] ON 

INSERT [dbo].[TransactionType] ([TransactionTypeID], [Description]) VALUES (1, N'PURCHASE')
INSERT [dbo].[TransactionType] ([TransactionTypeID], [Description]) VALUES (2, N'RENTAL')
SET IDENTITY_INSERT [dbo].[TransactionType] OFF
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [Name], [Created], [Updated]) VALUES (2, N'Administrator', CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2), CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Roles] ([RoleID], [Name], [Created], [Updated]) VALUES (3, N'Customer', CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2), CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [RoleID], [UserName], [Password], [Token], [FullName], [Created], [Active], [Email]) VALUES (1, 2, N'kevinadmin', N'X03MO1qnZdYdgyfeuILPmQ==', NULL, N'Kevin Omar Panameno Escalante', CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2), 1, N'omarpanameno.2@gmail.com')
INSERT [dbo].[Users] ([UserID], [RoleID], [UserName], [Password], [Token], [FullName], [Created], [Active], [Email]) VALUES (3, 3, N'kevin', N'bLdfZSqbUnmOts8iAQV8cw==', NULL, N'Kevin Panameno', CAST(N'1993-02-03T00:00:00.0000000' AS DateTime2), 1, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([MovieID], [Title], [Description], [Stock], [RentalPrice], [SalePrice], [Available], [Created], [UpdatedUserID], [Updated]) VALUES (1, N'Ford vs Ferrari', N'A movie based on a true story', 20, CAST(9.99 AS Decimal(18, 2)), CAST(59.99 AS Decimal(18, 2)), 0, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2), 1, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2))
INSERT [dbo].[Movies] ([MovieID], [Title], [Description], [Stock], [RentalPrice], [SalePrice], [Available], [Created], [UpdatedUserID], [Updated]) VALUES (2, N'American Gangster', N'New York City cop is charged with bringing down Harlem drug lord Frank Lucas', 12, CAST(9.99 AS Decimal(18, 2)), CAST(20.99 AS Decimal(18, 2)), 1, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2), 1, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2))
INSERT [dbo].[Movies] ([MovieID], [Title], [Description], [Stock], [RentalPrice], [SalePrice], [Available], [Created], [UpdatedUserID], [Updated]) VALUES (3, N'No Country for Old Men', N'Violence and mayhem ensue after a hunter stumbles upon a drug deal gone wrong and more than two million dollars in cash near the Rio Grande', 5, CAST(9.99 AS Decimal(18, 2)), CAST(30.99 AS Decimal(18, 2)), 1, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2), 1, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2))
INSERT [dbo].[Movies] ([MovieID], [Title], [Description], [Stock], [RentalPrice], [SalePrice], [Available], [Created], [UpdatedUserID], [Updated]) VALUES (7, N'Fast 2 Furious', N'Best movie of the franchise', 15, CAST(15.99 AS Decimal(18, 2)), CAST(79.99 AS Decimal(18, 2)), 1, CAST(N'2020-02-23T01:42:50.7753319' AS DateTime2), 1, CAST(N'2020-02-23T02:25:03.9766074' AS DateTime2))
INSERT [dbo].[Movies] ([MovieID], [Title], [Description], [Stock], [RentalPrice], [SalePrice], [Available], [Created], [UpdatedUserID], [Updated]) VALUES (8, N'The Fast and The Furious', N'Best movie of the franchise', 15, CAST(15.99 AS Decimal(18, 2)), CAST(79.99 AS Decimal(18, 2)), 0, CAST(N'2020-02-24T00:03:38.7180922' AS DateTime2), 1, CAST(N'2020-02-24T00:03:39.1024208' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Movies] OFF
SET IDENTITY_INSERT [dbo].[MovieTransaction] ON 

INSERT [dbo].[MovieTransaction] ([MovieTransactionID], [TransactionUserID], [PaymentType], [Branch], [Created], [UpdatedUserID], [Updated], [UserID]) VALUES (5, 1, N'Credit Card', N'San Salvador', CAST(N'2020-02-24T22:23:08.7011664' AS DateTime2), 1, CAST(N'2020-02-24T22:23:08.9159650' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[MovieTransaction] OFF
SET IDENTITY_INSERT [dbo].[MovieTransactionDetail] ON 

INSERT [dbo].[MovieTransactionDetail] ([MovieTransactionDetailID], [MovieTransactionID], [MovieID], [TransactionTypeID], [Returned], [MovieRentalTermID], [Quantity], [Price], [Created], [UpdatedUserID], [Updated]) VALUES (9, 5, 2, 2, 0, 1, 1, CAST(9.99 AS Decimal(18, 2)), CAST(N'2020-02-24T22:23:05.0664955' AS DateTime2), 1, CAST(N'2020-02-24T22:23:05.0665694' AS DateTime2))
INSERT [dbo].[MovieTransactionDetail] ([MovieTransactionDetailID], [MovieTransactionID], [MovieID], [TransactionTypeID], [Returned], [MovieRentalTermID], [Quantity], [Price], [Created], [UpdatedUserID], [Updated]) VALUES (10, 5, 3, 2, 0, 2, 1, CAST(9.99 AS Decimal(18, 2)), CAST(N'2020-02-24T22:23:06.3111319' AS DateTime2), 1, CAST(N'2020-02-24T22:23:06.3111399' AS DateTime2))
INSERT [dbo].[MovieTransactionDetail] ([MovieTransactionDetailID], [MovieTransactionID], [MovieID], [TransactionTypeID], [Returned], [MovieRentalTermID], [Quantity], [Price], [Created], [UpdatedUserID], [Updated]) VALUES (11, 5, 7, 1, 1, NULL, 3, CAST(79.99 AS Decimal(18, 2)), CAST(N'2020-02-24T22:23:07.0901694' AS DateTime2), 1, CAST(N'2020-02-24T22:23:07.0902060' AS DateTime2))
SET IDENTITY_INSERT [dbo].[MovieTransactionDetail] OFF
SET IDENTITY_INSERT [dbo].[MoviePriceLogs] ON 

INSERT [dbo].[MoviePriceLogs] ([MoviePriceLogID], [MovieID], [Title], [RentalPrice], [SalePrice], [Created], [UpdatedUserID], [Updated]) VALUES (1, 6, N'2 Fast 2 Furious', CAST(15.99 AS Decimal(18, 2)), CAST(99.99 AS Decimal(18, 2)), CAST(N'2020-02-23T01:31:21.3555245' AS DateTime2), 1, CAST(N'2020-02-23T01:31:21.9858731' AS DateTime2))
INSERT [dbo].[MoviePriceLogs] ([MoviePriceLogID], [MovieID], [Title], [RentalPrice], [SalePrice], [Created], [UpdatedUserID], [Updated]) VALUES (2, 6, N'ROQUE TEST 2 Fast 2 Furious', CAST(15.99 AS Decimal(18, 2)), CAST(99.99 AS Decimal(18, 2)), CAST(N'2020-02-23T01:38:29.5338230' AS DateTime2), 1, CAST(N'2020-02-23T01:38:29.5339404' AS DateTime2))
INSERT [dbo].[MoviePriceLogs] ([MoviePriceLogID], [MovieID], [Title], [RentalPrice], [SalePrice], [Created], [UpdatedUserID], [Updated]) VALUES (3, 7, N'The Fast and The Furious', CAST(15.99 AS Decimal(18, 2)), CAST(79.99 AS Decimal(18, 2)), CAST(N'2020-02-23T02:18:35.9349872' AS DateTime2), 1, CAST(N'2020-02-23T02:18:35.9351333' AS DateTime2))
SET IDENTITY_INSERT [dbo].[MoviePriceLogs] OFF
SET IDENTITY_INSERT [dbo].[MovieLikes] ON 

INSERT [dbo].[MovieLikes] ([MovieLikeID], [MovieID], [UserID], [Created]) VALUES (1, 1, 3, CAST(N'2020-02-22T23:20:47.1400000' AS DateTime2))
INSERT [dbo].[MovieLikes] ([MovieLikeID], [MovieID], [UserID], [Created]) VALUES (5, 2, 3, CAST(N'2020-02-22T23:20:47.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[MovieLikes] OFF
