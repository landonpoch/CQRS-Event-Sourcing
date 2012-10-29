ALTER TABLE [dbo].[EventLog]
    ADD CONSTRAINT [FK_EventLog_Aggregate] FOREIGN KEY ([AggregateId]) REFERENCES [dbo].[Aggregate] ([AggregateId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

