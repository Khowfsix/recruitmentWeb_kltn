using InsternShip.Api.Controllers;
using InsternShip.Data.ViewModels.Application;
using InsternShip.Data.ViewModels.Cv;
using InsternShip.Data.ViewModels.Position;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InsternShip.UnitTest.Application
{
    public class ApplicationControllerTests
    {
        private readonly Mock<IApplicationService> _mockApplicationService;
        private readonly ApplicationController _skillController;

        public ApplicationControllerTests()
        {
            _mockApplicationService = new Mock<IApplicationService>();
            _skillController = new ApplicationController(_mockApplicationService.Object);
        }

        [Fact]
        public async Task Application_Controller_Get_All_Test()
        {
            // Arrange
            string query = null!;
            var expecteds = new List<ApplicationViewModel>
            {
                new ApplicationViewModel
                {
                    ApplicationId = Guid.NewGuid(),
                    Cv = null!,
                    Position = null!,
                    DateTime = DateTime.Now,
                    Company_Status = "Accepted",
                    Candidate_Status = "Accepted",
                    Priority = "Accepted",
                },
                new ApplicationViewModel
                {
                    ApplicationId = Guid.NewGuid(),
                    Cv = null!,
                    Position = null!,
                    DateTime = DateTime.Now,
                    Company_Status = "Rejected",
                    Candidate_Status = "Rejected",
                    Priority = "Rejected",
                }
            };

            _mockApplicationService.Setup(service => service.GetAllApplications()).ReturnsAsync(expecteds);

            // Act
            var result = await _skillController.GetAllApplications(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var skillList = Assert.IsType<List<ApplicationViewModel>>(result.Value);
            Assert.Equal(2, skillList.Count);
        }

        [Fact]
        public async Task Application_Controller_Save_Application_Test()
        {
            // Arrange
            var input = new ApplicationAddModel
            {
                Cvid = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
            };

            var expected = new ApplicationViewModel
            {
                ApplicationId = Guid.NewGuid(),
                Cv = null!,
                Position = null!,
                DateTime = DateTime.Now,
                Company_Status = "Rejected",
                Candidate_Status = "Rejected",
                Priority = "Rejected",
            };

            _mockApplicationService.Setup(service => service.SaveApplication(input)).ReturnsAsync(expected);

            // Act
            var result = await _skillController.SaveApplication(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<ApplicationViewModel>(result.Value);
            Assert.Equal(expected.ApplicationId, created.ApplicationId);
            Assert.Equal(expected.Cv, created.Cv);
            Assert.Equal(expected.Position, created.Position);
            Assert.Equal(expected.DateTime, created.DateTime);
            Assert.Equal(expected.Company_Status, created.Company_Status);
            Assert.Equal(expected.Candidate_Status, created.Candidate_Status);
            Assert.Equal(expected.Priority, created.Priority);
        }

        [Fact]
        public async Task Application_Controller_Update_Application_Test()
        {
            // Arrange
            Guid skillId = Guid.NewGuid();
            var input = new ApplicationUpdateModel();
            var expected = true;

            _mockApplicationService.Setup(service => service.UpdateApplication(input, skillId)).ReturnsAsync(true);

            // Act
            var result = await _skillController.UpdateApplication(input, skillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Application_Controller_Delete_Application_Test()
        {
            // Arrange
            Guid skillId = Guid.NewGuid();
            var expected = true;

            _mockApplicationService.Setup(service => service.DeleteApplication(skillId)).ReturnsAsync(true);

            // Act
            var result = await _skillController.DeleteApplication(skillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted); // Add other property assertions
        }
    }
}
