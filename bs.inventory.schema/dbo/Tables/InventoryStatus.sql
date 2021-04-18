CREATE TABLE [dbo].[InventoryStatus] (
    [CorrelationId]   UNIQUEIDENTIFIER NOT NULL,
    [BasketRef]       UNIQUEIDENTIFIER NOT NULL,
    [CurrentState]    NVARCHAR (64)    NULL,
    [CreatedOn]       DATETIME2 (7)    NULL,
    [FailedOn]        DATETIME2 (7)    NULL,
    [ErrorMessage]    NVARCHAR (MAX)   NULL,
    [BasketPrice]     FLOAT (53)       DEFAULT ((0.0000000000000000e+000)) NOT NULL,
    [JsonBasketItems] NVARCHAR (MAX)   NULL,
    [OrderRef]        UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    CONSTRAINT [PK_InventoryStatus] PRIMARY KEY CLUSTERED ([CorrelationId] ASC)
);

