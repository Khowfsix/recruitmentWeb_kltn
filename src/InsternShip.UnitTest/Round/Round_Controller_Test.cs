using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Round;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Round
{
    public class RoundControllerTests
    {
        private readonly Mock<IRoundService> _mockRoundService;
        private readonly RoundController _RoundController;

        public RoundControllerTests()
        {
            _mockRoundService = new Mock<IRoundService>();
            _RoundController = new RoundController(_mockRoundService.Object);
        }

        [Fact]
        public async Task Round_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<RoundViewModel>
            {
                new RoundViewModel(),
                new RoundViewModel()
            };

            _mockRoundService.Setup(service => service.GetAllRounds(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _RoundController.GetAllRound(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var RoundList = Assert.IsType<List<RoundViewModel>>(result.Value);
            Assert.Equal(2, RoundList.Count);
        }

        [Fact]
        public async Task Round_Controller_Save_Round_Test()
        {
            // Arrange
            var input = new RoundAddModel();

            var expected = new RoundViewModel();

            _mockRoundService.Setup(service => service.SaveRound(input)).ReturnsAsync(expected);

            // Act
            var result = await _RoundController.SaveRound(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<RoundViewModel>(result.Value);
        }

        [Fact]
        public async Task Round_Controller_Update_Round_Test()
        {
            // Arrange
            Guid RoundId = Guid.NewGuid();
            var input = new RoundUpdateModel();

            var expected = true;

            _mockRoundService.Setup(service => service.UpdateRound(input, RoundId)).ReturnsAsync(true);

            // Act
            var result = await _RoundController.UpdateRound(input, RoundId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Round_Controller_Delete_Round_Test()
        {
            // Arrange
            Guid RoundId = Guid.NewGuid();
            var expected = true;

            _mockRoundService.Setup(service => service.DeleteRound(RoundId)).ReturnsAsync(true);

            // Act
            var result = await _RoundController.DeleteRound(RoundId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
