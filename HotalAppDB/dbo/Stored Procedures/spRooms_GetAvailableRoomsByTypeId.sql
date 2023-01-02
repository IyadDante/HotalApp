CREATE PROCEDURE [dbo].[spRooms_GetAvailableRoomsByTypeId]
	@startDate date,
	@endDate  date,
	@roomTypeID int
AS

begin
 set nocount on;

	select R.[Id], R.[RoomNumber], R.[RoomTypeId]
	from dbo.Rooms as R
	inner join RoomTypes as RT on RT.Id = R.RoomTypeId
	where RT.Id = @roomTypeID and R.Id not in
	(
		select B.RoomId from dbo.Booking as B where 
		(
			( @startDate < B.StartDate and @endDate   > B.EndDate) or 
			( @endDate  >= B.StartDate and @endDate   < B.EndDate) or
			( @endDate  >= B.StartDate and @startDate < B.EndDate)
		)
	);

end 