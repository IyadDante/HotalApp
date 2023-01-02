CREATE TABLE [dbo].[Booking]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RoomId] INT NOT NULL, 
    [GuestId] INT NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [CheckIn] BIT NOT NULL DEFAULT 0, 
    [TotalCost] MONEY NOT NULL, 
    CONSTRAINT [FK_Booking_Rooms] FOREIGN KEY (RoomId) REFERENCES Rooms(Id), 
    CONSTRAINT [FK_Booking_Gusets] FOREIGN KEY (GuestId) REFERENCES Guests(Id)
)
