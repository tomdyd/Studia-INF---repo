using Moq;
using projectDydaTomaszCore.Interfaces;
using projectDydaTomaszCore.Services;
using Xunit;
using projectDydaTomaszCore.Models;

namespace projectDydaTomasz.CoreTestes.Services
{

    public class TestServiceTest
    {
        [Fact]
        public void GetAllData_ShouldReturnUsersFromDatabase()
        {
            // Arrange
            var mockDatabaseConnection = new Mock<IDatabaseConnection<Test>>();
            var testData = new Test[] { };
            mockDatabaseConnection.Setup(x => x.GetUsers()).Returns(testData);

            var dataService = new DataService<Test>(mockDatabaseConnection.Object);

            // Act
            var result = dataService.GetAllData();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData, result);
        }
    }
}
