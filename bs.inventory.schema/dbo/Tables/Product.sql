CREATE TABLE [dbo].[Product] (
    [Id]         INT              IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100)   NOT NULL,
    [BrandId]    INT              NOT NULL,
    [CategoryId] INT              NOT NULL,
    [ModelYear]  INT              NOT NULL,
    [ListPrice]  FLOAT (53)       NOT NULL,
    [Reference]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_Brand_BrandId] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brand] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Product_BrandId]
    ON [dbo].[Product]([BrandId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Product_CategoryId]
    ON [dbo].[Product]([CategoryId] ASC);

