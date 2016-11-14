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
	[CreditRatingText] NVARCHAR(10) NULL
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);