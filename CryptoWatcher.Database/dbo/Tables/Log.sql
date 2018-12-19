CREATE TABLE [dbo].[Log] (
    [LogId]     UNIQUEIDENTIFIER NOT NULL,
    [Action]    NVARCHAR (50)    NOT NULL,
    [Entity]    NVARCHAR (50)    NOT NULL,
    [EntityId]  NVARCHAR (50)    NOT NULL,
    [Json]      NVARCHAR (MAX)   NOT NULL,
    [CreatedBy] NVARCHAR (50)    NOT NULL,
    [Time]      DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

