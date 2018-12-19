CREATE TABLE [dbo].[Indicator] (
    [IndicatorId] NVARCHAR (50)   NOT NULL,
    [UserId]      NVARCHAR (50)   NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NOT NULL,
    [Formula]     NVARCHAR (MAX)  NOT NULL,
    [CreatedBy]   NVARCHAR (50)   NOT NULL,
    [Time]        DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Indicator] PRIMARY KEY CLUSTERED ([IndicatorId] ASC)
);

