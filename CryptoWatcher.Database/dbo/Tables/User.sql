CREATE TABLE [dbo].[User] (
    [UserId]    NVARCHAR (50) NOT NULL,
    [CreatedBy] NVARCHAR (50) NOT NULL,
    [Time]      DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

