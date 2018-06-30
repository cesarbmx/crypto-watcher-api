CREATE TABLE [HangFire].[List] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [Key]      NVARCHAR (100) NOT NULL,
    [Value]    NVARCHAR (MAX) NULL,
    [ExpireAt] DATETIME       NULL,
    CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED ([Key] ASC, [Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt]
    ON [HangFire].[List]([ExpireAt] ASC) WHERE ([ExpireAt] IS NOT NULL);

