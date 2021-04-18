CREATE TABLE [dbo].[Store] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (30)  NOT NULL,
    [Phone]                 INT            NOT NULL,
    [StoreAddress_Street]   NVARCHAR (100) NULL,
    [StoreAddress_City]     NVARCHAR (100) NULL,
    [StoreAddress_Country]  NVARCHAR (100) NULL,
    [StoreAddress_PostCode] NVARCHAR (100) NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([Id] ASC)
);

