CREATE TABLE [dbo].[Notification] (
    [NotificationId]   UNIQUEIDENTIFIER NOT NULL,
    [UserId]           NVARCHAR (50)    NOT NULL,
    [PhoneNumber]      NVARCHAR (50)    NOT NULL,
    [Message]          NVARCHAR (50)    NOT NULL,
    [WhatsappSentTime] DATETIME2 (7)    NOT NULL,
    [CreatedBy]        NVARCHAR (50)    NOT NULL,
    [Time]             DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([NotificationId] ASC)
);

