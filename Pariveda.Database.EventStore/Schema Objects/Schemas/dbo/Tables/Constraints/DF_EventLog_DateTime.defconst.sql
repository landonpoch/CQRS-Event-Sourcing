ALTER TABLE [dbo].[EventLog]
    ADD CONSTRAINT [DF_EventLog_DateTime] DEFAULT (getdate()) FOR [DateTime];

