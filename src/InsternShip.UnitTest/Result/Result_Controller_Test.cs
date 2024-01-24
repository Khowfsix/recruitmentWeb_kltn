using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Result;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Result
{
    public class ResultControllerTests
    {
        private readonly Mock<IResultService> _mockResultService;
        private readonly ResultController _ResultController;

        public ResultControllerTests()
        {
            _mockResultService = new Mock<IResultService>();
            _ResultController = new ResultController(_mockResultService.Object);
        }

        [Fact]
        public async Task Result_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<ResultViewModel>
            {
                new ResultViewModel(),
                new ResultViewModel()
            };

            _mockResultService.Setup(service => service.GetAllResult()).ReturnsAsync(expecteds);

            // Act
            var result = await _ResultController.GetAllResult() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var ResultList = Assert.IsType<List<ResultViewModel>>(result.Value);
            Assert.Equal(2, ResultList.Count);
        }

        [Fact]
        public async Task Result_Controller_Save_Result_Test()
        {
            // Arrange
            var input = new ResultAddModel();

            var expected = new ResultViewModel();

            _mockResultService.Setup(service => service.SaveResult(input)).ReturnsAsync(expected);

            // Act
            var result = await _ResultController.SaveResult(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<ResultViewModel>(result.Value);
        }

        [Fact]
        public async Task Result_Controller_Update_Result_Test()
        {
            // Arrange
            Guid ResultId = Guid.NewGuid();
            var input = new ResultUpdateModel();

            var expected = true;

            _mockResultService.Setup(service => service.UpdateResult(input, ResultId)).ReturnsAsync(true);

            // Act
            var result = await _ResultController.UpdateResult(input, ResultId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Result_Controller_Delete_Result_Test()
        {
            // Arrange
            Guid ResultId = Guid.NewGuid();
            var expected = true;

            _mockResultService.Setup(service => service.DeleteResult(ResultId)).ReturnsAsync(true);

            // Act
            var result = await _ResultController.DeleteResult(ResultId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
