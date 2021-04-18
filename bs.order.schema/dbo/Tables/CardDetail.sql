CREATE TABLE [dbo].[CardDetail] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [CustomerId]            INT            NOT NULL,
    [CardType]              INT            NOT NULL,
    [CardHolderName]        NVARCHAR (MAX) NOT NULL,
    [CardNumber]            NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [Expiration]            DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [SecurityNumber]        INT            DEFAULT ((0)) NOT NULL,
    [CardNumberUnFormatted] BIGINT         DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    CONSTRAINT [PK_CardDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CardDetail_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CardDetail_CustomerId]
    ON [dbo].[CardDetail]([CustomerId] ASC);

