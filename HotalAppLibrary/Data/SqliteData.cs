﻿using HotalAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotalAppLibrary.Data
{
    public class SqliteData : IDatabaseData
    {
        public int BookGusetToRoom(int roomId, int guestId, DateTime startDate, DateTime endDate, decimal TotalRoomPrice)
        {
            throw new NotImplementedException();
        }

        public void CkeckInGuest(int bookingId)
        {
            throw new NotImplementedException();
        }

        public void GetAvailableRoomIds(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public int GetAvailableRoomsIdByRoomTypeId(DateTime startDate, DateTime endDate, int roomTypeID)
        {
            throw new NotImplementedException();
        }

        public List<RoomTypesModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public decimal GetRoomPrice(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public RoomTypesModel GetRoomTypesDetailById(int roomTypeId)
        {
            throw new NotImplementedException();
        }

        public int RegisterGuset(string FirstName, string LastName)
        {
            throw new NotImplementedException();
        }

        public List<BookingModel> SearchForBooking(DateTime startDate, DateTime endDate, string firstName, string lastName)
        {
            throw new NotImplementedException();
        }
    }
}