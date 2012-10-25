CREATE TABLE [dbo].[EventLog] (
    [AggregateId] UNIQUEIDENTIFIER NOT NULL,
    [Data]        VARBINARY (MAX)  NOT NULL,
    [Version]     INT              NOT NULL,
    [DateTime]    DATETIME         NOT NULL
);

