CREATE TABLE [dbo].[Payment] (
    [Id]                INT              IDENTITY (1, 1) NOT NULL,
    [CustomerId]        INT              NOT NULL,
    [CardDetailId]      INT              NULL,
    [PaymentRef]        UNIQUEIDENTIFIER NOT NULL,
    [Amount]            FLOAT (53)       NOT NULL,
    [TransactionDate]   DATETIME2 (7)    NULL,
    [PaymentType]       INT              NOT NULL,
    [TransactionStatus] INT              NOT NULL,
    [RefundedOn]        DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payment_CardDetail_CardDetailId] FOREIGN KEY ([CardDetailId]) REFERENCES [dbo].[CardDetail] ([Id]),
    CONSTRAINT [FK_Payment_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Payment_CardDetailId]
    ON [dbo].[Payment]([CardDetailId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Payment_CustomerId]
    ON [dbo].[Payment]([CustomerId] ASC);

