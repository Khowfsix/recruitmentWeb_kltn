using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Data.ViewModels.Room;
using InsternShip.Data.ViewModels.Shift;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Itrsinterview
{
    public class ItrsinterviewControllerTests
    {
        private readonly Mock<IItrsinterviewService> _mockItrsinterviewService;
        private readonly ItrsinterviewController _ItrsinterviewController;

        public ItrsinterviewControllerTests()
        {
            _mockItrsinterviewService = new Mock<IItrsinterviewService>();
            _ItrsinterviewController = new ItrsinterviewController(_mockItrsinterviewService.Object);
        }

        [Fact]
        public async Task Itrsinterview_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<ItrsinterviewViewModel>
            {
                new ItrsinterviewViewModel(),
                new ItrsinterviewViewModel()
            };

            _mockItrsinterviewService.Setup(service => service.GetAllItrsinterview()).ReturnsAsync(expecteds);

            // Act
            var result = await _ItrsinterviewController.GetAllItrsinterview(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var ItrsinterviewList = Assert.IsType<List<ItrsinterviewViewModel>>(result.Value);
            Assert.Equal(2, ItrsinterviewList.Count);
        }

        [Fact]
        public async Task Itrsinterview_Controller_Update_Itrsinterview_Test()
        {
            // Arrange
            Guid ItrsinterviewId = Guid.NewGuid();
            var input = new ItrsinterviewUpdateModel();

            var expected = true;

            _mockItrsinterviewService.Setup(service => service.UpdateItrsinterview(input, ItrsinterviewId, Guid.Empty)).ReturnsAsync(true);

            // Act
            var result = await _ItrsinterviewController.UpdateItrsinterview(input, ItrsinterviewId, Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Itrsinterview_Controller_Delete_Itrsinterview_Test()
        {
            // Arrange
            Guid ItrsinterviewId = Guid.NewGuid();
            var expected = true;

            _mockItrsinterviewService.Setup(service => service.DeleteItrsinterview(ItrsinterviewId)).ReturnsAsync(true);

            // Act
            var result = await _ItrsinterviewController.DeleteItrsinterview(ItrsinterviewId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
