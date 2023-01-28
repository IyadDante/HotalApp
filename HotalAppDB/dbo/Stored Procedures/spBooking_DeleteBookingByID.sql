CREATE PROCEDURE [dbo].[spBooking_DeleteBookingByID]
	@BookingId int 
AS
		delete [HotalAppDB].[dbo].[Booking]
		where [Id] = @BookingId
RETURN 0
