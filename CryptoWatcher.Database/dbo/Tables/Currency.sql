CREATE TABLE [dbo].[Currency] (
    [CurrencyId]          NVARCHAR (50) NOT NULL,
    [Symbol]              NVARCHAR (50) NOT NULL,
    [Name]                NVARCHAR (50) NOT NULL,
    [Rank]                SMALLINT      NOT NULL,
    [Price]               DECIMAL (18)  NOT NULL,
    [MarketCap]           DECIMAL (18)  NOT NULL,
    [Volume24H]           DECIMAL (18)  NOT NULL,
    [PercentageChange24H] DECIMAL (18)  NOT NULL,
    [CreatedBy]           NVARCHAR (50) NOT NULL,
    [Time]                DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CurrencyId] ASC)
);

