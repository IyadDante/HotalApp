using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotalAppLibrary.Data;
using Xunit;
using Autofac.Extras.Moq;
using HotalAppLibrary.Databases;
using HotalAppLibrary.Models;
using Moq;
using System.Data.Common;
using System.Data;

namespace HotelAppLibrary.Tests
{
    public class SqlDataTests : MockData
    {
        private DateTime pStartDate = DateTime.Now;
        private DateTime pEndDate = DateTime.Now.AddDays(1);
        private const string connectionStringName = "SqlDb";

        [Fact]
        public void XUnit_FirstUnitTestCase()
        {
            // Arrange
            int expected = 4;

            // Act
            double actual = 2 + 2;

            // Assurt
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void XUnit_SecondUnitTestCase(int x, int y)
        {
            // Arrange
            int expected = x;

            // Act
            double actual = y;

            // Assurt
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(21)]
        public void CkeckInGuest_MarkARoomAsBooked(int bookingId)
        {
            var sqlStatment = "[dbo].[spBookings_CheckIn]";
            var isStoredProsedure = true;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.SaveData(sqlStatment, new { bookingId }, connectionStringName, isStoredProsedure));

                var SqlDataClass = mock.Create<SqlData>();

                SqlDataClass.CkeckInGuest(bookingId);

                mock.Mock<ISqlDataAccess>()
                     .Verify(x => x.SaveData(sqlStatment, new { bookingId }, connectionStringName, isStoredProsedure), Times.Exactly(1));
            }
        }

        [Fact]
        public void GetAvailableRoomTypes_ShouldReturnAvailableRoomTypessList()
        {
            var sqlStatment = "[dbo].[spRoomTypes_GetAvailableRoomTypes]";
            var isStoredProsedure = true;
            var parametersObject = new { pStartDate, pEndDate };

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, It.Is<object>(param => param == parametersObject), connectionStringName, isStoredProsedure))
                    .Returns(GetAvailableRoomTypes());

                var SqlDataClass = mock.Create<SqlData>();

                SqlDataClass.GetAvailableRoomTypes(pStartDate, pEndDate);

                // Arrange 
                var expected = GetAvailableRoomTypes();

                //Act 
                var actual = SqlDataClass;

                // Assurt
                Assert.NotNull(actual);
                //Assert.Equal(actual.Count, expected.Count);
            }
        }

        [Fact]
        public void GetRoomTypesDetailById_ShouldReturnRoomDetails()
        {
            var roomTypeId = 2;
            var sqlStatment = "[dbo].[spRoomTypeDetails_GetById]";
            var returnedRoom = GetAvailableRoomTypes()[1];
            List<RoomTypesModel>? roomList = new List<RoomTypesModel>();

            using (var mock = AutoMock.GetLoose())
            {
                // Arrange
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, roomTypeId, connectionStringName, true))
                    .Returns(roomList);

                var SqlDataClass = mock.Create<SqlData>();

                // Act
                var actual = SqlDataClass.GetRoomTypesDetailById(roomTypeId);

                // Assert
                Assert.Equal(returnedRoom, actual);
            }

        }

        ////Yevhen Answer
        //[Fact]
        //public void GetRoomTypesDetailById_ShouldReturnRoomDetails()
        //{
        //    var roomTypeId = 2;
        //    var sqlStatment = "[dbo].[spRoomTypeDetails_GetById]";
        //    var returnedRoom = GetAvailableRoomTypes()[1];
        //    List<RoomTypesModel>? roomList = new List<RoomTypesModel>();

        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        object parameter = null;
        //        mock.Mock<ISqlDataAccess>()
        //            .Setup(x => x.LoadData<RoomTypesModel>(sqlStatment, It.IsAny<object>(), connectionStringName, true))
        //            .Callback<string, object, string, bool>((_, param1, _, _) => parameter = param1)
        //            .Returns(roomList);

        //        var SqlDataClass = mock.Create<SqlData>();

        //        // Arrange 
        //        var expected = returnedRoom;

        //        //Act 
        //        var actual = SqlDataClass.GetRoomTypesDetailById(roomTypeId);

        //        // Assert
        //        Assert.Equivalent(new { RoomTypeId = roomTypeId }, parameter);
        //        Assert.NotNull(actual);
        //        Assert.Equal(actual, expected);
        //    }

        //}
    }
}
