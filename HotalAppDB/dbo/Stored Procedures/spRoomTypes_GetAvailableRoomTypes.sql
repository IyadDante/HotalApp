CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableRoomTypes]
	@startDate datetime,
	@endDate datetime
AS
	set nocount on; 

	Select RT.Id, RT.Title, RT.Description, RT.Price from dbo.Rooms as R 
	inner join  dbo.RoomTypes as RT on RT.Id = R.Id  
	where R.Id not in (Select B.RoomId from Booking as B
	where (@startDate <  B.StartDate and @endDate   > B.EndDate) or 
	  ( @endDate  >= B.StartDate and @endDate   < B.EndDate) or
	  ( @endDate  >= B.StartDate and @startDate < B.EndDate))
	  group by RT.Id, RT.Title, RT.Description, RT.Price


RETURN 0
