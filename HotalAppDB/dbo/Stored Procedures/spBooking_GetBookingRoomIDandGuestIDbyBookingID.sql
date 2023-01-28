CREATE PROCEDURE [dbo].[spBooking_GetBookingRoomIDandGuestIDbyBookingID]
	@BookingID int
AS
	select RoomId, GuestId from [Booking] where Id = @BookingID
RETURN 0
