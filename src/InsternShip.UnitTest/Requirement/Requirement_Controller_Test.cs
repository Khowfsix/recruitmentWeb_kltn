using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Requirement;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Requirement
{
    public class RequirementControllerTests
    {
        private readonly Mock<IRequirementService> _mockRequirementService;
        private readonly RequirementController _RequirementController;

        public RequirementControllerTests()
        {
            _mockRequirementService = new Mock<IRequirementService>();
            _RequirementController = new RequirementController(_mockRequirementService.Object);
        }

        [Fact]
        public async Task Requirement_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<RequirementViewModel>
            {
                new RequirementViewModel(),
                new RequirementViewModel()
            };

            _mockRequirementService.Setup(service => service.GetAllRequirement()).ReturnsAsync(expecteds);

            // Act
            var result = await _RequirementController.GetAllRequirement() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var RequirementList = Assert.IsType<List<RequirementViewModel>>(result.Value);
            Assert.Equal(2, RequirementList.Count);
        }

        [Fact]
        public async Task Requirement_Controller_Save_Requirement_Test()
        {
            // Arrange
            var input = new RequirementAddModel();

            var expected = new RequirementViewModel();

            _mockRequirementService.Setup(service => service.SaveRequirement(input)).ReturnsAsync(expected);

            // Act
            var result = await _RequirementController.SaveRequirement(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<RequirementViewModel>(result.Value);
        }

        [Fact]
        public async Task Requirement_Controller_Update_Requirement_Test()
        {
            // Arrange
            Guid RequirementId = Guid.NewGuid();
            var input = new RequirementUpdateModel();

            var expected = true;

            _mockRequirementService.Setup(service => service.UpdateRequirement(input, RequirementId)).ReturnsAsync(true);

            // Act
            var result = await _RequirementController.UpdateRequirement(input, RequirementId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Requirement_Controller_Delete_Requirement_Test()
        {
            // Arrange
            Guid RequirementId = Guid.NewGuid();
            var expected = true;

            _mockRequirementService.Setup(service => service.DeleteRequirement(RequirementId)).ReturnsAsync(true);

            // Act
            var result = await _RequirementController.DeleteRequirement(RequirementId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
