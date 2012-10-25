CREATE TABLE [dbo].[OrderLine] (
    [OrderLineId] UNIQUEIDENTIFIER NOT NULL,
    [OrderId]     UNIQUEIDENTIFIER NOT NULL,
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [ProductName] VARCHAR (255)    NOT NULL
);

