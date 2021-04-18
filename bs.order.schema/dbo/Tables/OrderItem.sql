CREATE TABLE [dbo].[OrderItem] (
    [Id]              INT              IDENTITY (1, 1) NOT NULL,
    [OrderId]         INT              NOT NULL,
    [ProductRef]      UNIQUEIDENTIFIER NOT NULL,
    [ProductName]     NVARCHAR (MAX)   NOT NULL,
    [Quantity]        INT              NOT NULL,
    [IndividualPrice] FLOAT (53)       NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderItem_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_OrderItem_OrderId]
    ON [dbo].[OrderItem]([OrderId] ASC);

