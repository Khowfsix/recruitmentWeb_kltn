using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.SecurityQuestion;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.SecurityQuestion
{
    public class SecurityQuestionControllerTests
    {
        private readonly Mock<ISecurityQuestionService> _mockSecurityQuestionService;
        private readonly SecurityQuestionController _SecurityQuestionController;

        public SecurityQuestionControllerTests()
        {
            _mockSecurityQuestionService = new Mock<ISecurityQuestionService>();
            _SecurityQuestionController = new SecurityQuestionController(_mockSecurityQuestionService.Object);
        }

        [Fact]
        public async Task SecurityQuestion_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<SecurityQuestionViewModel>
            {
                new SecurityQuestionViewModel(),
                new SecurityQuestionViewModel()
            };

            _mockSecurityQuestionService.Setup(service => service.GetAllSecurityQuestion()).ReturnsAsync(expecteds);

            // Act
            var result = await _SecurityQuestionController.GetAllSecurityQuestion() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var SecurityQuestionList = Assert.IsType<List<SecurityQuestionViewModel>>(result.Value);
            Assert.Equal(2, SecurityQuestionList.Count);
        }

        [Fact]
        public async Task SecurityQuestion_Controller_Save_SecurityQuestion_Test()
        {
            // Arrange
            var input = new SecurityQuestionAddModel();

            var expected = new SecurityQuestionViewModel();

            _mockSecurityQuestionService.Setup(service => service.SaveSecurityQuestion(input)).ReturnsAsync(expected);

            // Act
            var result = await _SecurityQuestionController.SaveSecurityQuestion(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<SecurityQuestionViewModel>(result.Value);
        }

        [Fact]
        public async Task SecurityQuestion_Controller_Update_SecurityQuestion_Test()
        {
            // Arrange
            Guid SecurityQuestionId = Guid.NewGuid();
            var input = new SecurityQuestionUpdateModel();

            var expected = true;

            _mockSecurityQuestionService.Setup(service => service.UpdateSecurityQuestion(input, SecurityQuestionId)).ReturnsAsync(true);

            // Act
            var result = await _SecurityQuestionController.UpdateSecurityQuestion(input, SecurityQuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task SecurityQuestion_Controller_Delete_SecurityQuestion_Test()
        {
            // Arrange
            Guid SecurityQuestionId = Guid.NewGuid();
            var expected = true;

            _mockSecurityQuestionService.Setup(service => service.DeleteSecurityQuestion(SecurityQuestionId)).ReturnsAsync(true);

            // Act
            var result = await _SecurityQuestionController.DeleteSecurityQuestion(SecurityQuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
