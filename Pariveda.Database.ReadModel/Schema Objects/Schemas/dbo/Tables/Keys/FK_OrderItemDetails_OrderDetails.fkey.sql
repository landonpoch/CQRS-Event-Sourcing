ALTER TABLE [dbo].[OrderItemDetails]
    ADD CONSTRAINT [FK_OrderItemDetails_OrderDetails] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[OrderDetails] ([OrderId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

