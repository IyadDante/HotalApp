CREATE PROCEDURE [dbo].[spBooking_ListBooking]
	
AS

set nocount on; 

begin 

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT B.[Id]
	  ,R.[RoomNumber]
      ,G.[FirstName]
	  ,G.[LastName]
      ,[StartDate]
      ,[EndDate]
	  ,RT.[Id] as "RoomTypeId"
	  ,RT.[Title]
	  ,RT.[Description]
	  ,RT.[Price]
      ,[TotalCost]
	  ,[CheckIn]
  FROM [HotalAppDB].[dbo].[Booking] as B
  inner join [HotalAppDB].[dbo].[Rooms] as R on R.[Id] = B.[RoomId]
  inner join [HotalAppDB].[dbo].[Guests] as G on G.Id = B.GuestId
  inner join [HotalAppDB].[dbo].[RoomTypes] as RT on RT.Id = R.RoomTypeId

end

