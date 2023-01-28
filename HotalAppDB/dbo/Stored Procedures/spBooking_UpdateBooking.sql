CREATE PROCEDURE spBooking_UpdateBooking
  @BookingId int,
  @RoomId int,
  @GuestId int,
  @FirstName nvarchar (50),
  @LastName nvarchar (50),
  @StartDate datetime, 
  @EndDate datetime,
  @CheckIn bit, 
  @TotalCost dec
AS
BEGIN

	  update [HotalAppDB].[dbo].[Guests] 
		set FirstName = @FirstName, LastName = @LastName
		where Id = @GuestId

	  update [HotalAppDB].[dbo].[Booking]
		set [RoomId] = @RoomId, [GuestId] = @GuestId, [StartDate] = @StartDate, [EndDate] = @EndDate, [CheckIn] = @CheckIn, [TotalCost] = @TotalCost
		where [Id] = @BookingId

END