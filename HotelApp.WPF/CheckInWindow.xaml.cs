using HotalAppLibrary.Data;
using HotalAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelApp.WPF 
{
    /// <summary>
    /// Interaction logic for CheckInWindow.xaml
    /// </summary>
    public partial class CheckInWindow : Window
    {
        private readonly IDatabaseData _db;
        private BookingModel _bookingDetails = null;

        public string RoomNumber { get; set; }


        public CheckInWindow(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;
        }

        public void PopulateCheckInDetails (BookingModel data)
        {
            _bookingDetails = data;

            roomTypeText.Text = "Room Type: " + _bookingDetails.Title;
            roomTypeDetails.Text = "Room Details: " + _bookingDetails.Description;
            RoomNumber = _bookingDetails.RoomNumber;

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            string message = "Are you want to Check-In the guest?";
            string caption = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            if (MessageBox.Show(message, caption, buttons) == MessageBoxResult.Yes)
            {
                _db.CkeckInGuest(_bookingDetails.Id);
                this.Close();
            }
        }
    }
}
