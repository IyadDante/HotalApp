using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotalAppLibrary.Models
{
    public class BookingModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool CheckIn { get; set; }
        public decimal TotalCost { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
