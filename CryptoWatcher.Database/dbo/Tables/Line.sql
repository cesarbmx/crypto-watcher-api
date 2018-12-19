CREATE TABLE [dbo].[Line] (
    [LineId]      UNIQUEIDENTIFIER NOT NULL,
    [CurrencyId]  NVARCHAR (50)    NOT NULL,
    [IndicatorId] NVARCHAR (50)    NOT NULL,
    [Value]       DECIMAL (18)     NOT NULL,
    [AverageBuy]  DECIMAL (18, 2)  NOT NULL,
    [AverageSell] DECIMAL (18, 2)  NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [Time]        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Line] PRIMARY KEY CLUSTERED ([LineId] ASC)
);

