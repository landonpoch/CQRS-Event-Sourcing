﻿ALTER TABLE [dbo].[OrderLine]
    ADD CONSTRAINT [FK_OrderLine_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

