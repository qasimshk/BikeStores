CREATE TABLE [dbo].[Order] (
    [Id]                       INT              IDENTITY (1, 1) NOT NULL,
    [OrderRef]                 UNIQUEIDENTIFIER NOT NULL,
    [Status]                   INT              NOT NULL,
    [CancelledOn]              DATETIME2 (7)    NULL,
    [DeliveredOn]              DATETIME2 (7)    NULL,
    [ReasonOfCancellation]     NVARCHAR (MAX)   NULL,
    [PaymentId]                INT              NOT NULL,
    [CustomerId]               INT              NOT NULL,
    [DeliveryAddress_Street]   NVARCHAR (100)   NULL,
    [DeliveryAddress_City]     NVARCHAR (100)   NULL,
    [DeliveryAddress_Country]  NVARCHAR (100)   NULL,
    [DeliveryAddress_PostCode] NVARCHAR (100)   NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id]),
    CONSTRAINT [FK_Order_Payment_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [dbo].[Payment] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Order_CustomerId]
    ON [dbo].[Order]([CustomerId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Order_PaymentId]
    ON [dbo].[Order]([PaymentId] ASC);

