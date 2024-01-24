using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Recruiter;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Recruiter
{
    public class RecruiterControllerTests
    {
        private readonly Mock<IRecruiterService> _mockRecruiterService;
        private readonly RecruiterController _RecruiterController;

        public RecruiterControllerTests()
        {
            _mockRecruiterService = new Mock<IRecruiterService>();
            _RecruiterController = new RecruiterController(_mockRecruiterService.Object);
        }

        [Fact]
        public async Task Recruiter_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<RecruiterViewModel>
            {
                new RecruiterViewModel(),
                new RecruiterViewModel()
            };

            _mockRecruiterService.Setup(service => service.GetAllRecruiter()).ReturnsAsync(expecteds);

            // Act
            var result = await _RecruiterController.GetAllRecruiter(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var RecruiterList = Assert.IsType<List<RecruiterViewModel>>(result.Value);
            Assert.Equal(2, RecruiterList.Count);
        }

        [Fact]
        public async Task Recruiter_Controller_Save_Recruiter_Test()
        {
            // Arrange
            var input = new RecruiterAddModel();

            var expected = new RecruiterViewModel();

            _mockRecruiterService.Setup(service => service.SaveRecruiter(input)).ReturnsAsync(expected);

            // Act
            var result = await _RecruiterController.SaveRecruiter(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<RecruiterViewModel>(result.Value);
        }

        [Fact]
        public async Task Recruiter_Controller_Update_Recruiter_Test()
        {
            // Arrange
            Guid RecruiterId = Guid.NewGuid();
            var input = new RecruiterUpdateModel();

            var expected = true;

            _mockRecruiterService.Setup(service => service.UpdateRecruiter(input, RecruiterId)).ReturnsAsync(true);

            // Act
            var result = await _RecruiterController.UpdateRecruiter(input, RecruiterId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Recruiter_Controller_Delete_Recruiter_Test()
        {
            // Arrange
            Guid RecruiterId = Guid.NewGuid();
            var expected = true;

            _mockRecruiterService.Setup(service => service.DeleteRecruiter(RecruiterId)).ReturnsAsync(true);

            // Act
            var result = await _RecruiterController.DeleteRecruiter(RecruiterId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
