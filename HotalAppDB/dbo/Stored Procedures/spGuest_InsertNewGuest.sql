CREATE PROCEDURE [dbo].[spGuest_InsertNewGuest]
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
begin
	set nocount on; 

	if not exists (select 1 from dbo.Guests as G where (G.FirstName = @FirstName and G.LastName = @LastName))
		begin
			Insert into dbo.Guests (FirstName, LastName)
						   Values (@FirstName, @LastName)
	
			Select @@IDENTITY as NewGuestId
	    end
	else
		begin
			select G.Id from dbo.Guests as G where (G.FirstName = @FirstName and G.LastName = @LastName)
		end 
end					   


