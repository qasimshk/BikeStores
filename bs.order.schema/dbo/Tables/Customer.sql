CREATE TABLE [dbo].[Customer] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]               NVARCHAR (20)  NOT NULL,
    [LastName]                NVARCHAR (20)  NOT NULL,
    [DateOfBirth]             DATE           NOT NULL,
    [PhoneNumber]             NVARCHAR (11)  NOT NULL,
    [EmailAddress]            NVARCHAR (30)  NOT NULL,
    [BillingAddress_Street]   NVARCHAR (100) NULL,
    [BillingAddress_City]     NVARCHAR (100) NULL,
    [BillingAddress_Country]  NVARCHAR (100) NULL,
    [BillingAddress_PostCode] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customer_EmailAddress]
    ON [dbo].[Customer]([EmailAddress] ASC);

