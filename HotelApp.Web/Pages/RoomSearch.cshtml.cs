using HotalAppLibrary.Data;
using HotalAppLibrary.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Web.Pages
{
    public class RoomSearchModel : PageModel
    {
        // When we want get data from the form we set it to [BindProperty]
        // When we want add data to the link of the form [BindProperty(SupportsGet =true)] basicling posting them right on the link of the page

        private readonly IDatabaseData _db;

        //In order to capture the value coming for "OnPost" back we need to set the property to BindProperty
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet =true)]
        public DateTime StartDate { get; set; } = DateTime.Now.Date;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1).Date;

        [BindProperty(SupportsGet = true)]
        public bool SearchEnabled { get; set; } = false;

        public List<RoomTypesModel> AvailableRoomTypes { get; set; }

        //-------------------------------------//
        // Here is where we call our SqlData to use our methods
        // but First create a construtor and call IDatabaseData using dependecy injection 
        // there it will return an instance of SqlData

        public RoomSearchModel(IDatabaseData db)
        {
            _db = db;
        }
        public void OnGet() 
        {
            if (SearchEnabled == true) 
            {
                AvailableRoomTypes = _db.GetAvailableRoomTypes(StartDate, EndDate);
            }    
        }

        public IActionResult OnPost() 
        {
            return RedirectToPage(new 
            { 
                SearchEnabled = true, 
                StartDate = StartDate.ToString("yyyy-MM-dd"), 
                EndDate = EndDate.ToString("yyyy-MM-dd")
            });
        }

    }
}
