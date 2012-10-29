CREATE TABLE [dbo].[OrderItemDetails] (
    [OrderItemId] UNIQUEIDENTIFIER NOT NULL,
    [OrderId]     UNIQUEIDENTIFIER NOT NULL,
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [ProductName] NVARCHAR (MAX)   NOT NULL
);

