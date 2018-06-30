CREATE TABLE [HangFire].[Counter] (
    [Key]      NVARCHAR (100) NOT NULL,
    [Value]    INT            NOT NULL,
    [ExpireAt] DATETIME       NULL
);


GO
CREATE CLUSTERED INDEX [CX_HangFire_Counter]
    ON [HangFire].[Counter]([Key] ASC);

