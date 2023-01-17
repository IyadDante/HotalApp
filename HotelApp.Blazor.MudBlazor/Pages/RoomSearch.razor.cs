using HotalAppLibrary.Models;

namespace HotelApp.Blazor.MudBlazor.Pages
{
    public partial class RoomSearch 
    {
        private DateTime? startDate { get; set; }
        private DateTime? endDate { get; set; }
        private List<RoomTypesModel>? availableRooms { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string statusVisibility { get; set; } = "invisible";


        protected override async Task OnInitializedAsync()
        {
            startDate = DateTime.Today;
            endDate = DateTime.Today.AddDays(1);
        }
        void searchOnClick()
        {
            statusVisibility = "visible";

            availableRooms = _db.GetAvailableRoomTypes(startDate.Value.Date, endDate.Value.Date);
            StartDate = startDate.Value.ToShortDateString();
            EndDate = endDate.Value.ToShortDateString();
        }
    }
}
