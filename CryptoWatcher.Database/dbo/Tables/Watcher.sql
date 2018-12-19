CREATE TABLE [dbo].[Watcher] (
    [WatcherId]   UNIQUEIDENTIFIER NOT NULL,
    [UserId]      NVARCHAR (50)    NOT NULL,
    [CurrencyId]  NVARCHAR (50)    NOT NULL,
    [IndicatorId] NVARCHAR (50)    NOT NULL,
    [Value]       DECIMAL (18)     NOT NULL,
    [Buy]         DECIMAL (18)     NOT NULL,
    [Sell]        DECIMAL (18)     NOT NULL,
    [AverageBuy]  DECIMAL (18, 2)  NOT NULL,
    [AverageSell] DECIMAL (18, 2)  NOT NULL,
    [Enabled]     BIT              NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [Time]        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Watcher] PRIMARY KEY CLUSTERED ([WatcherId] ASC)
);

