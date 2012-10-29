-- =============================================
-- Author:		Landon Poch
-- Create date: 10/24/2012
-- Description:	Use this to get all the events
--				for a given aggregate.
-- =============================================
CREATE PROCEDURE GetEvents
	-- Add the parameters for the stored procedure here
	@AggregateId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		el.AggregateId,
		el.Data,
		el.[Version]
	FROM 
		EventLog el
	WHERE 
		el.AggregateId = @AggregateId
	ORDER BY 
		el.[Version]
END
