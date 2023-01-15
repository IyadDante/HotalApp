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
        private static DateTime pStartDate = DateTime.Now;
        private DateTime pEndDate = pStartDate.AddDays(1);

        private const string connectionStringName = "SqlDb";

        /// <summary>
        /// First we setup out our mocking for our database intance  
        /// We start off with our using statment of AutoMock.GetLoose() and save it in variable
        /// then use the mock var to build out our interface calls where we create the heart of our method that we want to test
        /// 
        /// </summary>
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

        /// <summary>
        /// NewUserID =  _db.RegisterGuset(newGuest.FirstName, newGuest.LastName);
        /// NewRoomID = _db.GetAvailableRoomsIdByRoomTypeId(startDate, endDate, roomTypeId);
        /// TotalRoomPrice = _db.GetRoomPrice(roomTypeId, startDate, endDate);
        /// BookingId = _db.BookGusetToRoom(NewRoomID, NewUserID, startDate, endDate, TotalRoomPrice);
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>

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

                var cls = mock.Create<SqlData>();

                cls.CkeckInGuest(bookingId);

                mock.Mock<ISqlDataAccess>()
                     .Verify(x => x.SaveData(sqlStatment, new { bookingId }, connectionStringName, isStoredProsedure), Times.Exactly(1));
            }
        }

        [Fact]
        public void GetAvailableRoomTypes_ShouldReturnAvailableRoomTypessList()
        {
            var sqlStatment = "[dbo].[spRoomTypes_GetAvailableRoomTypes]";
            var isStoredProsedure = true;

            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, new { pStartDate, pEndDate }, connectionStringName, isStoredProsedure))
                    .Returns(GetAvailableRoomTypes());

                var _dbMock = mock.Create<SqlData>();

                _dbMock.GetAvailableRoomTypes(pStartDate, pEndDate);

                // Arrange 
                var expected = GetAvailableRoomTypes();

                //Act 
                var actual = _dbMock.GetAvailableRoomTypes(pStartDate, pEndDate);

                // Assurt
                Assert.NotNull(actual);
                Assert.Equal(actual.Count, expected.Count);
            }
        }

        [Fact]
        public void GetRoomTypesDetailById_ShouldReturnRoomDetails()
        {
            var roomTypeId = 2;
            var sqlStatment = "[dbo].[spRoomTypeDetails_GetById]";
            var returnedRoom = GetAvailableRoomTypes()[1] ;
            List<RoomTypesModel>? roomList = new List<RoomTypesModel>();
            
            using (var mock = AutoMock.GetLoose()) 
            {
                // Arrange - configure the mock
                mock.Mock<ISqlDataAccess>().Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, roomTypeId, connectionStringName, true)).Returns(roomList);
                var sut = mock.Create<SqlData>();

                // Act
                var actual = sut.GetRoomTypesDetailById(roomTypeId);

                // Assert
                Assert.Equal(returnedRoom, actual);
            }

        }

        //[Fact]
        //public void Test()
        //{
        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        // Arrange - configure the mock
        //        mock.Mock<IDependency>().Setup(x => x.GetValue()).Returns("expected value");
        //        var sut = mock.Create<SystemUnderTest>();

        //        // Act
        //        var actual = sut.DoWork();

        //        // Assert - assert on the mock
        //        mock.Mock<IDependency>().Verify(x => x.GetValue());
        //        Assert.Equal("expected value", actual);
        //    }
        //}

        //public class SystemUnderTest
        //{
        //    private readonly IDependency dependency;

        //    public SystemUnderTest(IDependency strings)
        //    {
        //        this.dependency = strings;
        //    }

        //    public string DoWork()
        //    {
        //        return this.dependency.GetValue();
        //    }
        //}

        //public interface IDependency
        //{
        //    string GetValue();
        //}

    }
}
