CREATE TABLE [dbo].[Order] (
    [OrderId]    UNIQUEIDENTIFIER NOT NULL,
    [UserId]     NVARCHAR (50)    NOT NULL,
    [CurrencyId] NVARCHAR (50)    NOT NULL,
    [WatcherId]  NVARCHAR (50)    NOT NULL,
    [OrderType]  SMALLINT         NOT NULL,
    [Quantity]   DECIMAL (18)     NOT NULL,
    [Status]     SMALLINT         NOT NULL,
    [CreatedBy]  NVARCHAR (50)    NOT NULL,
    [Time]       DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);

