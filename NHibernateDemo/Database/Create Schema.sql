USE [NHibernateDemo]
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Order'))
DROP TABLE dbo.[Order];

CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NULL,
	[Ordered] [datetime] NULL,
	[Shipped] [datetime] NULL,
	[Street] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Province] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Customer'))
DROP TABLE dbo.[Customer];

CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Points] [int] NULL,
	[HasGoldStatus] [bit] NULL,
	[MemberSince] [date] NULL,
	[CreditRating] [nchar](20) NULL,
	[AverageRating] [decimal](18, 4) NULL,
	[Street] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Province] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

ALTER TABLE [dbo].[Order] WITH CHECK 
    ADD CONSTRAINT [FK_Order_Customer] 
        FOREIGN KEY([CustomerId])
        REFERENCES [dbo].[Customer] ([Id]);
GO


