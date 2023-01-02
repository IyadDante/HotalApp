CREATE PROCEDURE [dbo].[spRoomType_GetPriceByRoomTypeId]
	@RoomTypeId int = 0
	
AS
begin
	set nocount on;

	SELECT RT.Price FROM [dbo].[RoomTypes] as RT
	where RT.Id = @RoomTypeId
end