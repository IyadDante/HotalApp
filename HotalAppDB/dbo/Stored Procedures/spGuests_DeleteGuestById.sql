CREATE PROCEDURE [dbo].[spGuests_DeleteGuestById]
	@GuestId int 
AS

		delete [HotalAppDB].[dbo].[Guests] 
		where Id = @GuestId

RETURN 0
