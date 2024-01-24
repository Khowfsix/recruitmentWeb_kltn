using FakeItEasy;
using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Interview;
using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Data.ViewModels.Room;
using InsternShip.Data.ViewModels.Shift;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Interview
{
    public class InterviewControllerTests
    {
        private readonly Mock<IItrsinterviewService> _mockItrsInterviewService;
        private readonly Mock<IInterviewService> _mockInterviewService;
        private readonly InterviewController _InterviewController;

        public InterviewControllerTests()
        {
            _mockItrsInterviewService = new Mock<IItrsinterviewService>();
            _mockInterviewService = new Mock<IInterviewService>();
            _InterviewController = new InterviewController(_mockInterviewService.Object, _mockItrsInterviewService.Object);
        }

        [Fact]
        public async Task Interview_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<InterviewViewModel>
            {
                new InterviewViewModel(),
                new InterviewViewModel()
            };

            _mockInterviewService.Setup(service => service.GetAllInterview("")).ReturnsAsync(expecteds);

            // Act
            var result = await _InterviewController.GetAllInterview(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var InterviewList = Assert.IsType<List<InterviewViewModel>>(result.Value);
            Assert.Equal(2, InterviewList.Count);
        }

        [Fact]
        public async Task Interview_Controller_Save_Interview_Ok_Test()
        {
            // Arrange
            Guid applicationId = Guid.NewGuid();
            var request = new InterviewWithTimeAddModel
            {
                Interview = new InterviewAddModel
                {
                    InterviewerId = Guid.NewGuid(),
                    RecruiterId = Guid.NewGuid(),
                    ApplicationId = Guid.NewGuid(),
                    ItrsinterviewId = Guid.NewGuid(),
                    Notes = "string",
                    ResultId = Guid.NewGuid(),
                },
                ITRS = new ItrsinterviewAddModel
                {
                    DateInterview = DateTime.Today,
                    ShiftId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                }
            };

            var expectedResponseITRS = new ItrsinterviewViewModel
            {
                ItrsinterviewId = Guid.NewGuid(),
                DateInterview = DateTime.Today,
                Room = new RoomViewModel
                {
                    RoomId = Guid.NewGuid(),
                    RoomName = "string"
                },
                Shift = new ShiftViewModel
                {
                    ShiftId = Guid.NewGuid(),
                    ShiftTimeStart = 1,
                    ShiftTimeEnd = 2
                }
            };

            var expectedResponseInterview = new InterviewViewModel
            {
                InterviewId = Guid.NewGuid(),
            };

            _mockItrsInterviewService.Setup(service => service.SaveItrsinterview(request.ITRS, request.Interview.InterviewerId)).ReturnsAsync(expectedResponseITRS);
            _mockInterviewService.Setup(service => service.SaveInterview(request.Interview)).ReturnsAsync(expectedResponseInterview);

            // Act
            var result = await _InterviewController.SaveInterview(request, applicationId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var savedInterview = Assert.IsType<InterviewViewModel>(result.Value);
            Assert.Equal(expectedResponseInterview.InterviewId, savedInterview.InterviewId);
            // Add other property assertions
        }

        [Fact]
        public async Task Interview_Controller_Save_Interview_BadRequest_Test()
        {
            // Arrange
            var request = new InterviewWithTimeAddModel
            {
                Interview = null!,
                ITRS = new ItrsinterviewAddModel
                {
                    DateInterview = DateTime.Today,
                    ShiftId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                }
            };

            // Act
            var result = await _InterviewController.SaveInterview(request, Guid.NewGuid()) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task Interview_Controller_Save_Interview_Conflict_Test()
        {
            // Arrange
            var request = new InterviewWithTimeAddModel
            {
                Interview = new InterviewAddModel
                {
                    InterviewerId = Guid.NewGuid(),
                    RecruiterId = Guid.NewGuid(),
                    ApplicationId = Guid.NewGuid(),
                    ItrsinterviewId = Guid.NewGuid(),
                    Notes = "string",
                    ResultId = Guid.NewGuid(),
                },
                ITRS = new ItrsinterviewAddModel
                {
                    DateInterview = DateTime.Today,
                    ShiftId = Guid.NewGuid(),
                    RoomId = Guid.NewGuid(),
                }
            };

            var expectedResponseITRS = new ItrsinterviewViewModel
            {
                ItrsinterviewId = Guid.NewGuid(),
                DateInterview = DateTime.Today,
                Room = new RoomViewModel
                {
                    RoomId = Guid.NewGuid(),
                    RoomName = "string"
                },
                Shift = new ShiftViewModel
                {
                    ShiftId = Guid.NewGuid(),
                    ShiftTimeStart = 1,
                    ShiftTimeEnd = 2
                }
            };

            _mockItrsInterviewService.Setup(service => service.SaveItrsinterview(request.ITRS, request.Interview.InterviewerId)).ReturnsAsync(expectedResponseITRS);

            // Act
            var result = await _InterviewController.SaveInterview(request, Guid.NewGuid()) as StatusCodeResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(409, result.StatusCode);
        }

        [Fact]
        public async Task Interview_Controller_Delete_Interview_Test()
        {
            // Arrange
            Guid InterviewId = Guid.NewGuid();
            var expected = true;

            _mockInterviewService.Setup(service => service.DeleteInterview(InterviewId)).ReturnsAsync(true);

            // Act
            var result = await _InterviewController.DeleteInterview(InterviewId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
