CREATE PROCEDURE [dbo].[spBookings_CheckIn]
	@Id int
AS
begin 

	set nocount on;

	update dbo.Booking 
	set CheckIn = 1
	where Id = @Id;
	
end

