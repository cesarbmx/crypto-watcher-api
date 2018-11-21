CREATE TABLE [HangFire].[JobParameter] (
    [JobId] BIGINT         NOT NULL,
    [Name]  NVARCHAR (40)  NOT NULL,
    [Value] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED ([JobId] ASC, [Name] ASC),
    CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY ([JobId]) REFERENCES [HangFire].[Job] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);

