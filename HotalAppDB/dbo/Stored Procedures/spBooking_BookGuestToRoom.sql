CREATE PROCEDURE [dbo].[spBooking_BookGuestToRoom]
	@aRoomId int,
	@aGuestId int,
	@aStartDate date,
	@aEndDate date,
	@aCheckIn bit,
	@aTotalCost money
AS
begin
	set nocount on; 

	  insert into Booking (RoomId, GuestId, StartDate, EndDate, CheckIn, TotalCost)
						   Values (@aRoomId, @aGuestId,@aStartDate,@aEndDate,@aCheckIn,@aTotalCost);

	Select @@IDENTITY as BookingId

end