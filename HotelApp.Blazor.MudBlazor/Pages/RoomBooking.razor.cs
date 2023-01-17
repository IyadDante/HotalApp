using HotalAppLibrary.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Blazor.MudBlazor.Pages
{
    public partial class RoomBooking
    {
        [Parameter]
        [SupplyParameterFromQuery]
        public int roomTypeId { get; set; }
        [Parameter]
        [Required]
        [SupplyParameterFromQuery]
        public DateTime startDate { get; set; }
        [Parameter]
        [Required]
        [SupplyParameterFromQuery]
        public DateTime endDate { get; set; }

        public RoomTypesModel roomTypeModel { get; set; }

        public int NewUserID { get; set; }

        public int NewRoomID { get; set; }

        public int BookingId { get; set; }

        public decimal TotalRoomPrice { get; set; }

        bool success;

        ReqiredInputs newGuest = new ReqiredInputs();

        public class ReqiredInputs
        {
            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string LastName { get; set; }
        }

        protected override void OnInitialized()
        {
            roomTypeModel = _db.GetRoomTypesDetailById(roomTypeId);
        }

        private void OnValidSubmit(EditContext context)
        {
            success = true;
            StateHasChanged();
            searchOnClick();
            NavManager.NavigateTo("/Index");
        }

        public void searchOnClick()
        {
            NewUserID = _db.RegisterGuset(newGuest.FirstName, newGuest.LastName);
            NewRoomID = _db.GetAvailableRoomsIdByRoomTypeId(startDate, endDate, roomTypeId);
            TotalRoomPrice = _db.GetRoomPrice(roomTypeId, startDate, endDate);
            BookingId = _db.BookGusetToRoom(NewRoomID, NewUserID, startDate, endDate, TotalRoomPrice);
        }
    }
}
