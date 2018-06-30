CREATE TABLE [HangFire].[Server] (
    [Id]            NVARCHAR (100) NOT NULL,
    [Data]          NVARCHAR (MAX) NULL,
    [LastHeartbeat] DATETIME       NOT NULL,
    CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_HangFire_Server_LastHeartbeat]
    ON [HangFire].[Server]([LastHeartbeat] ASC);

