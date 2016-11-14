IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Customer'))
DROP TABLE dbo.Customer;

CREATE TABLE [dbo].[Customer] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
	[AverageRating] DECIMAL(18,4) NULL,
    [Points] INT NULL,
	[HasGoldStatus] BIT NULL,
	[MemberSince] DATE NULL,
	[CreditRating] NVARCHAR(10) NULL,
	[CreditRatingText] NVARCHAR(10) NULL,
    [Street] NVARCHAR (50) NULL,
    [City] NVARCHAR (50) NULL,
    [Province] NVARCHAR (50) NULL,
    [Country] NVARCHAR (50) NULL
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);