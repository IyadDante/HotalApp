using Autofac.Extras.Moq;
using HotalAppLibrary.Criteria;
using HotalAppLibrary.Data;
using HotalAppLibrary.Databases;
using HotalAppLibrary.Models;
using Moq;
using Xunit;

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
            // Arrange
            var sqlStatment = "[dbo].[spBookings_CheckIn]";
            var isStoredProsedure = true;

            using (var mock = AutoMock.GetLoose())
            {
                // Direct passing an instance of anonymous type isn't working because the anonymous type { Id = bookingId } created in the unit tests
                // assembly is not the same that the anonymous type with the same signature but created in a
                // separate assembly. See https://stackoverflow.com/questions/60723140/calling-equals-on-anonymous-type-depends-on-which-assembly-the-object-was-create
                // So this setup will not match:
                // mock.Mock<ISqlDataAccess>().Setup(x => x.SaveData(sqlStatment, new { Id = bookingId }, connectionStringName, isStoredProsedure));
                // There are 2 workarounds:
                // 1. Extract non-anonymous (named) type and use it in both: SqlData.CkeckInGuest method and in the Moq setup (I strongly recommend this approach - see the next unit test)
                // 2. Use It.IsAny with callback in the setup (I wouldn't recommend this approach - the result code is too fragile), see the implementation below:

                object actualParameter = null;

                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.SaveData(sqlStatment, It.IsAny<object>(), connectionStringName, isStoredProsedure))
                    .Callback<string?, object, string, bool>((_, parameter, _, _) => actualParameter = parameter);

                var SqlDataClass = mock.Create<SqlData>();

                // Act
                SqlDataClass.CkeckInGuest(bookingId);

                // Assert
                mock.Mock<ISqlDataAccess>()
                     .Verify(x => x.SaveData(sqlStatment, It.IsAny<object>(), connectionStringName, isStoredProsedure), Times.Exactly(1));

                // Assert.Equal will fail here - see the reason description above 
                Assert.Equivalent(new { Id = bookingId }, actualParameter);
            }
        }

        [Fact]
        public void GetAvailableRoomTypes_ShouldReturnAvailableRoomTypessList()
        {
            // Arrange 
            var sqlStatment = "[dbo].[spRoomTypes_GetAvailableRoomTypes]";
            var isStoredProsedure = true;
            var expected = GetAvailableRoomTypes();

            using (var mock = AutoMock.GetLoose())
            {

                // Extraction of a named type AvailableRoomTypesCriteria significantly simplifies the unit test:
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, It.Is<AvailableRoomTypesCriteria>(param => param.StartDate == pStartDate && param.EndDate == pEndDate), connectionStringName, isStoredProsedure))
                    .Returns(expected);

                var SqlDataClass = mock.Create<SqlData>();

                //Act 
                var actual = SqlDataClass.GetAvailableRoomTypes(pStartDate, pEndDate);

                // Assurt
                Assert.Equal(actual, expected);
            }
        }

        //[Fact]
        //public void GetRoomTypesDetailById_ShouldReturnRoomDetails()
        //{
        //    var roomTypeId = 2;
        //    var sqlStatment = "[dbo].[spRoomTypeDetails_GetById]";
        //    var returnedRoom = GetAvailableRoomTypes()[1];
        //    List<RoomTypesModel>? roomList = new List<RoomTypesModel>();

        //    using (var mock = AutoMock.GetLoose())
        //    {
        //        // Arrange
        //        mock.Mock<ISqlDataAccess>()
        //            .Setup(x => x.LoadData<RoomTypesModel, dynamic>(sqlStatment, roomTypeId, connectionStringName, true))
        //            .Returns(roomList);

        //        var SqlDataClass = mock.Create<SqlData>();

        //        // Act
        //        var actual = SqlDataClass.GetRoomTypesDetailById(roomTypeId);

        //        // Assert
        //        Assert.Equal(returnedRoom, actual);
        //    }

        //}

        //Yevhen Answer
        [Fact]
        public void GetRoomTypesDetailById_ShouldReturnRoomDetails()
        {
            // Arrange
            var roomTypeId = 2;
            var sqlStatement = "[dbo].[spRoomTypeDetails_GetById]";
            var availableRoomTypes = GetAvailableRoomTypes();

            using (var mock = AutoMock.GetLoose())
            {
                object? parameter = null;
                mock.Mock<ISqlDataAccess>()
                    .Setup(x => x.LoadData1<RoomTypesModel>(sqlStatement, It.IsAny<object>(), connectionStringName, true))
                    .Callback<string, object, string, bool>((_, param1, _, _) => parameter = param1)
                    .Returns(availableRoomTypes);

                var sqlDataClass = mock.Create<SqlData>();

                var expected = availableRoomTypes[0];

                // Act 
                var actual = sqlDataClass.GetRoomTypesDetailById(roomTypeId);

                // Assert
                Assert.Equivalent(new { RoomTypeId = roomTypeId }, parameter);
                Assert.Equal(actual, expected);
            }
        }
    }
}
