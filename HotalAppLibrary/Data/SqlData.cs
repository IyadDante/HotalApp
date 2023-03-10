using HotalAppLibrary.Criteria;
using HotalAppLibrary.Databases;
using HotalAppLibrary.Models;

namespace HotalAppLibrary.Data
{
    public class SqlData : IDatabaseData
    {
        private readonly ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        ////  **********************        Iyad Answer       **********************  ////
        public List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            List<RoomTypesModel> output = new();

            output = _db.LoadData<RoomTypesModel, dynamic>("[dbo].[spRoomTypes_GetAvailableRoomTypes]",
                                                         new AvailableRoomTypesCriteria { StartDate = startDate, EndDate = endDate },
                                                         connectionStringName,
                                                         true
                                                         );
            return output;
        }

        public int RegisterGuset(string FirstName, string LastName)
        {
            int NewUserId = 0;

            NewUserId = _db.LoadData<int, dynamic>("[dbo].[spGuest_InsertNewGuest]",
                                       new { FirstName, LastName },
                                       connectionStringName,
                                       true).First();
            return NewUserId;
        }

        public void GetAvailableRoomIds(DateTime startDate, DateTime endDate)
        {

            RoomTypesModel availableRoomId = _db.LoadData<RoomTypesModel, dynamic>("[dbo].[spRoomTypes_GetAvailableRoomIDs]",
                                                        new { startDate, endDate },
                                                        connectionStringName,
                                                        true
                                                        ).First();
        }

        public RoomTypesModel GetRoomTypesDetailById(int roomTypeId)
        {
            RoomTypesModel model = _db.LoadData1<RoomTypesModel>("[dbo].[spRoomTypeDetails_GetById]",
                                                         new { RoomTypeId = roomTypeId },
                                                         connectionStringName,
                                                         true).First();
            return model;
        }

        public int BookGusetToRoom(int roomId, int guestId, DateTime startDate, DateTime endDate, decimal TotalRoomPrice)
        {
            var aCheckIn = false;
            var BookingId = 0;

            BookingId = _db.LoadData<int, dynamic>("[dbo].[spBooking_BookGuestToRoom]",
                                                new
                                                {
                                                    aRoomId = roomId,
                                                    aGuestId = guestId,
                                                    aStartDate = startDate,
                                                    aEndDate = endDate,
                                                    aCheckIn,
                                                    aTotalCost = TotalRoomPrice
                                                }, connectionStringName, true).First();
            return BookingId;
        }

        public List<BookingModel> SearchForBooking(DateTime startDate, DateTime endDate, string firstName, string lastName)
        {
            List<BookingModel> searchResult = new List<BookingModel>();


            searchResult = _db.LoadData<BookingModel, dynamic>("[dbo].[spBooking_Search]",
                                                               new { startDate, endDate, firstName, lastName },
                                                               connectionStringName,
                                                               true
                                                               );
            return searchResult;
        }

        public int GetAvailableRoomsIdByRoomTypeId(DateTime startDate, DateTime endDate, int roomTypeID)
        {
            RoomTypesModel AvailableRoomsId = new RoomTypesModel();

            AvailableRoomsId = _db.LoadData<RoomTypesModel, dynamic>("[dbo].[spRooms_GetAvailableRoomsByTypeId]",
                                                        new { startDate, endDate, roomTypeID },
                                                        connectionStringName,
                                                        true).First();
            return AvailableRoomsId.Id;
        }

        public decimal GetRoomPrice(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            decimal roomPrice = 0;

            roomPrice = _db.LoadData<Decimal, dynamic>("[dbo].[spRoomType_GetPriceByRoomTypeId]",
                                                    new { roomTypeId },
                                                    connectionStringName,
                                                    true).First();

            var timeOfStay = endDate.Date.Subtract(startDate.Date);

            // Here we convert TimeSpan to Int then To Decimal
            var timeOfStayInt = Convert.ToInt32(timeOfStay.Days);
            var timeOfStayDecimal = Convert.ToDecimal(timeOfStayInt);

            // To calculate the total Price 
            var totalRoomPrice = roomPrice * timeOfStayDecimal;

            return totalRoomPrice;
        }

        public List<GusetsModel> GetGuestsList()
        {
            List<GusetsModel> gusetsList = new ();

            gusetsList = _db.SelectData<GusetsModel, dynamic>("[dbo].[spGuest_ListGuest]", connectionStringName);

            return gusetsList;
        }

        public List<BookingModel> GetBookingsDetailsList()
        {
            List<BookingModel> gusetBooking = new();

            gusetBooking = _db.SelectData<BookingModel, dynamic>("[dbo].[spBooking_ListBooking]", connectionStringName);

            return gusetBooking;
        }


        public List<BookingModel> UpdateBooking( int BookingId, int RoomId, int GuestId, string FirstName, string LastName, DateTime startDate, DateTime endDate, bool CheckIn, decimal TotalCost)
        {
            List<BookingModel> bookingList = new();

            bookingList = _db.LoadData<BookingModel, dynamic>("[dbo].[spBooking_UpdateBooking]",
                                                        new { BookingId, RoomId, GuestId, FirstName, LastName, startDate, endDate, CheckIn, TotalCost },
                                                        connectionStringName,
                                                        true);

            return bookingList;
        }

        public List<BookingModel> GetBookingRoomIDandGuestIDbyBookingID(int BookingId)
        {
            List<BookingModel> bookingList = new();

            bookingList = _db.LoadData<BookingModel, dynamic>("[dbo].[spBooking_GetBookingRoomIDandGuestIDbyBookingID]",
                                                        new { BookingId },
                                                        connectionStringName,
                                                        true);

            return bookingList;
        }

        public void CkeckInGuest(int bookingId)
        {
            _db.SaveData("[dbo].[spBookings_CheckIn]",
                          new { Id = bookingId },
                          connectionStringName,
                          true);
        }


        public void DeleteGuestById(int GuestId)
        {

             _db.SaveData("[dbo].[spGuests_DeleteGuestById]",
                                                        new { GuestId },
                                                        connectionStringName,
                                                        true);
        }

        public void DeleteBookingByID(int BookingId)
        {

            _db.SaveData("[dbo].[spBooking_DeleteBookingByID]",
                                                       new { BookingId },
                                                       connectionStringName,
                                                       true);
        }






        ////  **********************        Yevhen Answer       **********************  ////

        //public List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        //{
        //    List<RoomTypesModel> output = new();

        //    output = _db.LoadData<RoomTypesModel>("[dbo].[spRoomTypes_GetAvailableRoomTypes]",
        //                                                 new { startDate, endDate },
        //                                                 connectionStringName,
        //                                                 true
        //                                                 );
        //    return output;
        //}

        //public int RegisterGuset(string FirstName, string LastName)
        //{
        //    int NewUserId = 0;

        //    NewUserId = _db.LoadData<int>("[dbo].[spGuest_InsertNewGuest]",
        //                               new { FirstName, LastName },
        //                               connectionStringName,
        //                               true).First();
        //    return NewUserId;
        //}

        //public void CkeckInGuest(int bookingId)
        //{
        //    _db.SaveData("[dbo].[spBookings_CheckIn]",
        //                  new { Id = bookingId },
        //                  connectionStringName,
        //                  true);
        //}

        //public void GetAvailableRoomIds(DateTime startDate, DateTime endDate)
        //{

        //    RoomTypesModel availableRoomId = _db.LoadData<RoomTypesModel>("[dbo].[spRoomTypes_GetAvailableRoomIDs]",
        //                                                new { startDate, endDate },
        //                                                connectionStringName,
        //                                                true
        //                                                ).First();
        //}

        //public RoomTypesModel GetRoomTypesDetailById(int roomTypeId)
        //{
        //    RoomTypesModel model = new RoomTypesModel();

        //    model = _db.LoadData<RoomTypesModel>("[dbo].[spRoomTypeDetails_GetById]",
        //                                                 new { RoomTypeId = roomTypeId },
        //                                                 connectionStringName,
        //                                                 true).First();
        //    return model;
        //}

        //public int BookGusetToRoom(int roomId, int guestId, DateTime startDate, DateTime endDate, decimal TotalRoomPrice)
        //{
        //    var aCheckIn = false;
        //    var BookingId = 0;

        //    BookingId = _db.LoadData<int>("[dbo].[spBooking_BookGuestToRoom]",
        //                                        new
        //                                        {
        //                                            aRoomId = roomId,
        //                                            aGuestId = guestId,
        //                                            aStartDate = startDate,
        //                                            aEndDate = endDate,
        //                                            aCheckIn,
        //                                            aTotalCost = TotalRoomPrice
        //                                        }, connectionStringName, true).First();
        //    return BookingId;
        //}

        //public List<BookingModel> SearchForBooking(DateTime startDate, DateTime endDate, string firstName, string lastName)
        //{
        //    List<BookingModel> searchResult = new List<BookingModel>();


        //    searchResult = _db.LoadData<BookingModel>("[dbo].[spBooking_Search]",
        //                                                       new { startDate, endDate, firstName, lastName },
        //                                                       connectionStringName,
        //                                                       true
        //                                                       );
        //    return searchResult;
        //}

        //public int GetAvailableRoomsIdByRoomTypeId(DateTime startDate, DateTime endDate, int roomTypeID)
        //{
        //    RoomTypesModel AvailableRoomsId = new RoomTypesModel();

        //    AvailableRoomsId = _db.LoadData<RoomTypesModel>("[dbo].[spRooms_GetAvailableRoomsByTypeId]",
        //                                                new { startDate, endDate, roomTypeID },
        //                                                connectionStringName,
        //                                                true).First();
        //    return AvailableRoomsId.Id;
        //}

        //public decimal GetRoomPrice(int roomTypeId, DateTime startDate, DateTime endDate)
        //{
        //    decimal roomPrice = 0;

        //    roomPrice = _db.LoadData<Decimal>("[dbo].[spRoomType_GetPriceByRoomTypeId]",
        //                                            new { roomTypeId },
        //                                            connectionStringName,
        //                                            true).First();

        //    var timeOfStay = endDate.Date.Subtract(startDate.Date);

        //    // Here we convert TimeSpan to Int then To Decimal
        //    var timeOfStayInt = Convert.ToInt32(timeOfStay.Days);
        //    var timeOfStayDecimal = Convert.ToDecimal(timeOfStayInt);

        //    // To calculate the total Price 
        //    var totalRoomPrice = roomPrice * timeOfStayDecimal;

        //    return totalRoomPrice;
        //}
        ////  **********************        Yevhen Answer       **********************  ////

    }
}
