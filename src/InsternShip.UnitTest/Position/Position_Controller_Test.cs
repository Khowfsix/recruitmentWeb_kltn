using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Position;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Position
{
    public class PositionControllerTests
    {
        private readonly Mock<IPositionService> _mockPositionService;
        private readonly PositionController _PositionController;

        public PositionControllerTests()
        {
            _mockPositionService = new Mock<IPositionService>();
            _PositionController = new PositionController(_mockPositionService.Object);
        }

        [Fact]
        public async Task Position_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<PositionViewModel>
            {
                new PositionViewModel(),
                new PositionViewModel()
            };

            _mockPositionService.Setup(service => service.GetAllPositions(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _PositionController.GetAllPositions(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var PositionList = Assert.IsType<List<PositionViewModel>>(result.Value);
            Assert.Equal(2, PositionList.Count);
        }

        [Fact]
        public async Task Position_Controller_Save_Position_Test()
        {
            // Arrange
            var input = new PositionAddModel();

            var expected = new PositionViewModel();

            _mockPositionService.Setup(service => service.AddPosition(input)).ReturnsAsync(expected);

            // Act
            var result = await _PositionController.AddPosition(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<PositionViewModel>(result.Value);
        }

        [Fact]
        public async Task Position_Controller_Update_Position_Test()
        {
            // Arrange
            Guid PositionId = Guid.NewGuid();
            var input = new PositionUpdateModel();

            var expected = true;

            _mockPositionService.Setup(service => service.UpdatePosition(input, PositionId)).ReturnsAsync(true);

            // Act
            var result = await _PositionController.UpdatePosition(input, PositionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Position_Controller_Delete_Position_Test()
        {
            // Arrange
            Guid PositionId = Guid.NewGuid();
            var expected = true;

            _mockPositionService.Setup(service => service.RemovePosition(PositionId)).ReturnsAsync(true);

            // Act
            var result = await _PositionController.RemovePosition(PositionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
