CREATE TABLE [dbo].[Stock] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [ProductId] INT NOT NULL,
    [StockIn]   INT NOT NULL,
    [StockOut]  INT NOT NULL,
    [StoreId]   INT NOT NULL,
    CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Stock_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Stock_ProductId]
    ON [dbo].[Stock]([ProductId] ASC);

