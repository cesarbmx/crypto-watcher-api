CREATE TABLE [HangFire].[JobQueue] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [JobId]     BIGINT        NOT NULL,
    [Queue]     NVARCHAR (50) NOT NULL,
    [FetchedAt] DATETIME      NULL,
    CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED ([Queue] ASC, [Id] ASC)
);

