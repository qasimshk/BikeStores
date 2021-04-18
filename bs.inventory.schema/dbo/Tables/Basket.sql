CREATE TABLE [dbo].[Basket] (
    [Id]        INT              IDENTITY (1, 1) NOT NULL,
    [BasketRef] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Basket] PRIMARY KEY CLUSTERED ([Id] ASC)
);

