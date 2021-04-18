CREATE TABLE [dbo].[OrderState] (
    [CorrelationId]       UNIQUEIDENTIFIER NOT NULL,
    [CurrentState]        NVARCHAR (64)    NULL,
    [CreatedOn]           DATETIME2 (7)    NULL,
    [FailedOn]            DATETIME2 (7)    NULL,
    [TransactionRef]      UNIQUEIDENTIFIER NOT NULL,
    [CustomerId]          INT              NOT NULL,
    [OrderId]             INT              NOT NULL,
    [PaymentId]           INT              NOT NULL,
    [ErrorMessage]        NVARCHAR (MAX)   NULL,
    [BasketRef]           UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [OrderRef]            UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [JsonOrderItems]      NVARCHAR (MAX)   NULL,
    [PaymentType]         INT              DEFAULT ((0)) NOT NULL,
    [CardDetailId]        INT              DEFAULT ((0)) NOT NULL,
    [JsonCardDetails]     NVARCHAR (MAX)   NULL,
    [JsonDeliveryAddress] NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_OrderState] PRIMARY KEY CLUSTERED ([CorrelationId] ASC)
);

