using HotalAppLibrary.Models;

namespace HotalAppLibrary.Data
{
    public interface IDatabaseData
    {
        int BookGusetToRoom(int roomId, int guestId, DateTime startDate, DateTime endDate, decimal TotalRoomPrice );
        void CkeckInGuest(int bookingId);
        void GetAvailableRoomIds(DateTime startDate, DateTime endDate);
        List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        int GetAvailableRoomsIdByRoomTypeId(DateTime startDate, DateTime endDate, int roomTypeID);
        RoomTypesModel GetRoomTypesDetailById(int roomTypeId);
        int RegisterGuset(string FirstName, string LastName);
        List<BookingModel> SearchForBooking(DateTime startDate, DateTime endDate, string firstName, string lastName);
        decimal GetRoomPrice(int roomTypeId, DateTime startDate, DateTime endDate);


    }
}