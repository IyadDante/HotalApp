/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select 1 from dbo.RoomTypes)
begin 

insert into RoomTypes (Title,Description, Price)
values  ('King Size Bed', 'A room with a king sixe bed and a window view.', 100),
        ('Two Queen Size Ben','A room with two queen size bed', 115),
        ('Execlusive Suite','Two room, each with one king size bed and a window view', 205)
end

if not exists (select 1 from dbo.Rooms)
begin
    
    declare @RoomId1 int;
    declare @RoomId2 int;
    declare @RoomId3 int;

    select @RoomId1 = 1 from dbo.RoomTypes where title = 'King Size Bed'
    select @RoomId2 = 1 from dbo.RoomTypes where title = 'King Size Bed'
    select @RoomId3 = 1 from dbo.RoomTypes where title = 'King Size Bed'

    insert into Rooms (RoomNumber,RoomTypeId)
    values ('101', @RoomId1),
           ('102', @RoomId1),
           ('103', @RoomId1),
           ('202', @RoomId2),
           ('202', @RoomId2),
           ('103', @RoomId3)
end