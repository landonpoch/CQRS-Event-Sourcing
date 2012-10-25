-- =============================================
-- Author:		Landon Poch
-- Create date: 10/24/2012
-- Description:	Use this stored procedure to
--				capture any events that are
--				occuring in the system and save
--				them to the event store.
-- =============================================
CREATE PROCEDURE [dbo].[WriteEvent] 
	@AggregateId uniqueidentifier,
	@Type as varchar(50),
	@Data as varbinary(max),
	@ExpectedVersion as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    BEGIN TRAN
    BEGIN TRY
		-- Get the version number for the aggregate
		DECLARE @CurrentVersion as INT
		SELECT @CurrentVersion = [Version] 
		FROM [Aggregate]
		WHERE AggregateId = @AggregateId
		
		-- If this is a new aggregate set it to version 0
		IF @CurrentVersion IS NULL
		BEGIN
			SET @CurrentVersion = 0
			INSERT INTO [Aggregate] (AggregateId, [Type], [Version])
			VALUES (@AggregateId, @Type, @CurrentVersion)
		END
		
		-- Check to make sure we are inserting the right version
		IF @ExpectedVersion != @CurrentVersion
		BEGIN
			RAISERROR ('Optimistic concurrency error while trying to add event to the store', 16, 1)
		END
		
		--TODO: See if you can make this work with multiple events
		-- Foreach
			-- Insert the new event into the event store
			INSERT INTO EventLog (AggregateId, Data, [Version])
			VALUES (@AggregateId, @Data, @ExpectedVersion)
			
			-- Increment the aggregate's version number
			UPDATE [Aggregate]
			SET [Version] = @CurrentVersion + 1
			WHERE AggregateId = @AggregateId
		-- End
		
		COMMIT TRAN
	
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		RAISERROR ('Optimistic concurrency error while trying to add event to the store', 16, 1)
	END CATCH
	
END
