using HotalAppLibrary.Data;
using HotalAppLibrary.Models;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDatabaseData _db;
        public DateTime SelectedStartDate { get; set; }
        public DateTime SelectedEndDate { get; set; }

        public int BookingId { get; set; }
        public List<BookingModel> bookings { get; set; }
        public BookingModel SelectedBookings { get; set; }

        public MainWindow(IDatabaseData db)
        {
            InitializeComponent();
            _db = db;
        }

        private void searchForGuest_Click(object sender, RoutedEventArgs e)
        {
            SelectedStartDate = (DateTime)startDatePicker.SelectedDate;
            SelectedEndDate = (DateTime)endDatePicker.SelectedDate;
            
            bookings = _db.SearchForBooking(SelectedStartDate, SelectedEndDate, firstNameText.Text, lastNameText.Text);
            searchList.ItemsSource = bookings;
        }


        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {


            var checkInWindow = App.serviceProvider.GetService<CheckInWindow>();
            SelectedBookings = (BookingModel)((Button)e.Source).DataContext;
            checkInWindow.PopulateCheckInDetails(SelectedBookings);
            checkInWindow.Show();
        }
    }
}
