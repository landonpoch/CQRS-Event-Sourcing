﻿ALTER TABLE [dbo].[Aggregate]
    ADD CONSTRAINT [PK_Aggregate] PRIMARY KEY CLUSTERED ([AggregateId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

