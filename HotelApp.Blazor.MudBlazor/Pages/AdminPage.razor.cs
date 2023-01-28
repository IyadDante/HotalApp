using HotalAppLibrary.Data;
using HotalAppLibrary.Databases;
using HotalAppLibrary.Models;
using System.Net.NetworkInformation;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System.Reflection;
using static MudBlazor.CategoryTypes;

namespace HotelApp.Blazor.MudBlazor.Pages
{
    public partial class AdminPage
    {
        public List<GusetsModel> guestsList { get; set; }
        public List<BookingModel> bookingList { get; set; }
        SfGrid<BookingModel> BookingGrid { get; set; }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int pRoomTypeId { get; set; }
        public int NewUserID { get; set; }
        public int NewRoomID { get; set; }
        public decimal TotalRoomPrice { get; set; }
        public int BookingId { get; set; }

        


        private List<object> toolBar = new List<object> {"Add", "Edit", "Update", "Cancel", "Delete", "Search"};

        int addCounter = new();
        int saveCounter = new();

        protected override void OnInitialized()
        {
            guestsList = _db.GetGuestsList();
            bookingList = _db.GetBookingsDetailsList();
        }

        public async Task DataboundHandler(object args)
        {
            await BookingGrid.AutoFitColumns();
        }

        /*
            NewUserID = _db.RegisterGuset(FirstName, LastName);
            NewRoomID = _db.GetAvailableRoomsIdByRoomTypeId(pStartDate, pEndDate, pRoomTypeId);
            TotalRoomPrice = _db.GetRoomPrice(NewRoomID, pStartDate, pEndDate);
            BookingId = _db.BookGusetToRoom(NewRoomID, NewUserID, pStartDate, pEndDate, TotalRoomPrice);

            return RedirectToPage("/Index");
         */

        public void ActionBegin(ActionEventArgs<BookingModel> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.BeginEdit)
            {
                // Triggers before editing operation starts
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Add)
            {
                // Triggers before add operation starts
                var value = args;
                addCounter++;
            }
                        
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Delete)
            {
                // Triggers before delete operation starts
                var value = args.Data.Id;
            }
        }

        public void ActionComplete(ActionEventArgs<BookingModel> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Cancel)
            {
                // Triggers once cancel operation completes
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Save)
            {
                var bookingID = args.Data.Id;

                if (bookingID == 0)
                {
                    NewUserID = _db.RegisterGuset(args.Data.FirstName, args.Data.LastName);
                    NewRoomID = _db.GetAvailableRoomsIdByRoomTypeId(args.Data.StartDate, args.Data.EndDate, args.Data.RoomTypeId);
                    TotalRoomPrice = _db.GetRoomPrice(NewRoomID, args.Data.StartDate, args.Data.EndDate);
                    BookingId = _db.BookGusetToRoom(NewRoomID, NewUserID, args.Data.StartDate, args.Data.EndDate, TotalRoomPrice);
                     
                    Refresh();
                }
                else 
                {
                    // We need to get RoomID and GuestID 

                    var resultValue = _db.GetBookingRoomIDandGuestIDbyBookingID(args.Data.Id).First();
                    var updatedRoomId = resultValue.RoomId; 
                    var updatedGuestId = resultValue.GuestId;

                    _db.UpdateBooking(args.Data.Id, updatedRoomId, updatedGuestId,  args.Data.FirstName,  args.Data.LastName, args.Data.StartDate, args.Data.EndDate,  args.Data.CheckIn,  args.Data.TotalCost);
                    Refresh();
                }
            }
        }

        public void Refresh()
        {
            bookingList = _db.GetBookingsDetailsList();
        }

    }
}
