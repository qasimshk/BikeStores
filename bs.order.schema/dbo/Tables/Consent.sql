CREATE TABLE [dbo].[Consent] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [CustomerId]     INT NOT NULL,
    [ContactByEmail] BIT NOT NULL,
    [ContactByText]  BIT NOT NULL,
    [ContactByCall]  BIT NOT NULL,
    [ContactByPost]  BIT NOT NULL,
    CONSTRAINT [PK_Consent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Consent_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Consent_CustomerId]
    ON [dbo].[Consent]([CustomerId] ASC);

