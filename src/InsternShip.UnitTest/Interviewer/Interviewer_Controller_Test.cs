using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Interviewer;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Interviewer
{
    public class InterviewerControllerTests
    {
        private readonly Mock<IInterviewerService> _mockInterviewerService;
        private readonly InterviewerController _InterviewerController;

        public InterviewerControllerTests()
        {
            _mockInterviewerService = new Mock<IInterviewerService>();
            _InterviewerController = new InterviewerController(_mockInterviewerService.Object);
        }

        [Fact]
        public async Task Interviewer_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<InterviewerViewModel>
            {
                new InterviewerViewModel(),
                new InterviewerViewModel()
            };

            _mockInterviewerService.Setup(service => service.GetAllInterviewer()).ReturnsAsync(expecteds);

            // Act
            var result = await _InterviewerController.GetAllInterviewer(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var InterviewerList = Assert.IsType<List<InterviewerViewModel>>(result.Value);
            Assert.Equal(2, InterviewerList.Count);
        }

        [Fact]
        public async Task Interviewer_Controller_Save_Interviewer_Test()
        {
            // Arrange
            var input = new InterviewerAddModel();

            var expected = new InterviewerViewModel();

            _mockInterviewerService.Setup(service => service.SaveInterviewer(input)).ReturnsAsync(expected);

            // Act
            var result = await _InterviewerController.SaveInterviewer(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<InterviewerViewModel>(result.Value);
        }

        [Fact]
        public async Task Interviewer_Controller_Update_Interviewer_Test()
        {
            // Arrange
            Guid InterviewerId = Guid.NewGuid();
            var input = new InterviewerUpdateModel();

            var expected = true;

            _mockInterviewerService.Setup(service => service.UpdateInterviewer(input, InterviewerId)).ReturnsAsync(true);

            // Act
            var result = await _InterviewerController.UpdateInterviewer(input, InterviewerId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Interviewer_Controller_Delete_Interviewer_Test()
        {
            // Arrange
            Guid InterviewerId = Guid.NewGuid();
            var expected = true;

            _mockInterviewerService.Setup(service => service.DeleteInterviewer(InterviewerId)).ReturnsAsync(true);

            // Act
            var result = await _InterviewerController.DeleteInterviewer(InterviewerId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
