using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Room;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Room
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomService> _mockRoomService;
        private readonly RoomController _RoomController;

        public RoomControllerTests()
        {
            _mockRoomService = new Mock<IRoomService>();
            _RoomController = new RoomController(_mockRoomService.Object);
        }

        [Fact]
        public async Task Room_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<RoomViewModel>
            {
                new RoomViewModel(),
                new RoomViewModel()
            };

            _mockRoomService.Setup(service => service.GetAllRoom()).ReturnsAsync(expecteds);

            // Act
            var result = await _RoomController.GetAllRoom() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var RoomList = Assert.IsType<List<RoomViewModel>>(result.Value);
            Assert.Equal(2, RoomList.Count);
        }

        [Fact]
        public async Task Room_Controller_Save_Room_Test()
        {
            // Arrange
            var input = new RoomAddModel();

            var expected = new RoomViewModel();

            _mockRoomService.Setup(service => service.SaveRoom(input)).ReturnsAsync(expected);

            // Act
            var result = await _RoomController.SaveRoom(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<RoomViewModel>(result.Value);
        }

        [Fact]
        public async Task Room_Controller_Update_Room_Test()
        {
            // Arrange
            Guid RoomId = Guid.NewGuid();
            var input = new RoomUpdateModel();

            var expected = true;

            _mockRoomService.Setup(service => service.UpdateRoom(input, RoomId)).ReturnsAsync(true);

            // Act
            var result = await _RoomController.UpdateRoom(input, RoomId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Room_Controller_Delete_Room_Test()
        {
            // Arrange
            Guid RoomId = Guid.NewGuid();
            var expected = true;

            _mockRoomService.Setup(service => service.DeleteRoom(RoomId)).ReturnsAsync(true);

            // Act
            var result = await _RoomController.DeleteRoom(RoomId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
