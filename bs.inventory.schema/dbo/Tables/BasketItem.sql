CREATE TABLE [dbo].[BasketItem] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [BasketId]  INT        NOT NULL,
    [Amount]    FLOAT (53) NOT NULL,
    [Quantity]  INT        NOT NULL,
    [ProductId] INT        DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BasketItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasketItem_Basket_BasketId] FOREIGN KEY ([BasketId]) REFERENCES [dbo].[Basket] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_BasketItem_BasketId]
    ON [dbo].[BasketItem]([BasketId] ASC);

