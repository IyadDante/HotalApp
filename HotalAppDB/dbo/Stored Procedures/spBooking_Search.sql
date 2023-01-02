CREATE PROCEDURE [dbo].[spBooking_Search]
	@startDate date,
	@endDate date, 
	@firstName nvarchar (50),
	@lastName nvarchar(50)
AS
	set nocount on; 

select B.Id, B.RoomId, B.GuestId,B.StartDate,B.EndDate, B.CheckIn,B.TotalCost,
	   G.FirstName,G.LastName,
	   R.RoomNumber,R.RoomTypeId,
	   RT.Title,RT.[Description],RT.Price
from Rooms as R
inner join RoomTypes as RT on RT.Id = R.RoomTypeId
inner join Booking as B on B.RoomId = R.Id
inner join Guests as G on G.Id = B.GuestId
where (G.FirstName = @firstName And G.LastName = @lastName) And ((@startDate <  B.StartDate and @endDate   > B.EndDate) or 
	  ( @endDate  >= B.StartDate and @endDate   < B.EndDate) or
	  ( @endDate  >= B.StartDate and @startDate < B.EndDate))


RETURN 0