CREATE PROCEDURE [dbo].[spRoomTypeDetails_GetById]
	@RoomTypeId int
AS
	
begin 
 set nocount on; 
	select [Id], [Title], [Description], [Price] from dbo.RoomTypes as RT where RT.Id = @RoomTypeId 
end