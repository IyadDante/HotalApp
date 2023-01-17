using HotalAppLibrary.Data;
using HotalAppLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace HotelApp.Web.Pages
{
    public class BookRoomModel : PageModel
    {
        // NOTE:-
        // When we want get data from the form we set it to [BindProperty]
        // When we want add data to the link of the form [BindProperty(SupportsGet =true)] basicling posting them right on the link of the page

        private readonly IDatabaseData _db;

        [BindProperty(SupportsGet = true)]
        public DateTime pStartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime pEndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int pRoomTypeId { get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        
        [BindProperty]
        public string LastName { get; set; }

        public RoomTypesModel RoomType { get; set; }

        public int NewUserID { get; set; }

        public int NewRoomID { get; set; }

        public int BookingId { get; set; }

        public decimal TotalRoomPrice { get; set; }


        public BookRoomModel(IDatabaseData db)
        {
            _db = db;
        }


        public void OnGet()
        {

            if (pRoomTypeId > 0) 
            {
                RoomType = _db.GetRoomTypesDetailById(pRoomTypeId);
            }
        }
        public IActionResult OnPost()
        {
            NewUserID = _db.RegisterGuset(FirstName, LastName);
            NewRoomID = _db.GetAvailableRoomsIdByRoomTypeId(pStartDate, pEndDate, pRoomTypeId);
            TotalRoomPrice = _db.GetRoomPrice(NewRoomID, pStartDate, pEndDate);
            BookingId = _db.BookGusetToRoom(NewRoomID, NewUserID, pStartDate, pEndDate, TotalRoomPrice);
            return RedirectToPage("/Index");
        }

    }
}
