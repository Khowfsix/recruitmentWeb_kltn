using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.CandidateJoinEvent;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.CandidateJoinEvent
{
    public class CandidateJoinEventControllerTests
    {
        private readonly Mock<ICandidateJoinEventService> _mockCandidateJoinEventService;
        private readonly CandidateJoinEventController _CandidateJoinEventController;

        public CandidateJoinEventControllerTests()
        {
            _mockCandidateJoinEventService = new Mock<ICandidateJoinEventService>();
            _CandidateJoinEventController = new CandidateJoinEventController(_mockCandidateJoinEventService.Object);
        }

        [Fact]
        public async Task CandidateJoinEvent_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CandidateJoinEventViewModel>
            {
                new CandidateJoinEventViewModel(),
                new CandidateJoinEventViewModel()
            };

            _mockCandidateJoinEventService.Setup(service => service.GetAllCandidateJoinEvents()).ReturnsAsync(expecteds);

            // Act
            var result = await _CandidateJoinEventController.GetAllCandidateJoinEvents() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var CandidateJoinEventList = Assert.IsType<List<CandidateJoinEventViewModel>>(result.Value);
            Assert.Equal(2, CandidateJoinEventList.Count);
        }

        [Fact]
        public async Task CandidateJoinEvent_Controller_Save_CandidateJoinEvent_Test()
        {
            // Arrange
            var input = new CandidateJoinEventAddModel();

            var expected = new CandidateJoinEventViewModel();

            _mockCandidateJoinEventService.Setup(service => service.SaveCandidateJoinEvent(input)).ReturnsAsync(expected);

            // Act
            var result = await _CandidateJoinEventController.SaveCandidateJoinEvent(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CandidateJoinEventViewModel>(result.Value);
        }

        [Fact]
        public async Task CandidateJoinEvent_Controller_Update_CandidateJoinEvent_Test()
        {
            // Arrange
            Guid CandidateJoinEventId = Guid.NewGuid();
            var input = new CandidateJoinEventUpdateModel();

            var expected = true;

            _mockCandidateJoinEventService.Setup(service => service.UpdateCandidateJoinEvent(input, CandidateJoinEventId)).ReturnsAsync(true);

            // Act
            var result = await _CandidateJoinEventController.UpdateCandidateJoinEvent(input, CandidateJoinEventId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task CandidateJoinEvent_Controller_Delete_CandidateJoinEvent_Test()
        {
            // Arrange
            Guid CandidateJoinEventId = Guid.NewGuid();
            var expected = true;

            _mockCandidateJoinEventService.Setup(service => service.DeleteCandidateJoinEvent(CandidateJoinEventId)).ReturnsAsync(true);

            // Act
            var result = await _CandidateJoinEventController.DeleteCandidateJoinEvent(CandidateJoinEventId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
