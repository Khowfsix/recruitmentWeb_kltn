using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.SecurityAnswer;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.SecurityAnswer
{
    public class SecurityAnswerControllerTests
    {
        private readonly Mock<ISecurityAnswerService> _mockSecurityAnswerService;
        private readonly SecurityAnswerController _SecurityAnswerController;

        public SecurityAnswerControllerTests()
        {
            _mockSecurityAnswerService = new Mock<ISecurityAnswerService>();
            _SecurityAnswerController = new SecurityAnswerController(_mockSecurityAnswerService.Object);
        }

        [Fact]
        public async Task SecurityAnswer_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<SecurityAnswerViewModel>
            {
                new SecurityAnswerViewModel(),
                new SecurityAnswerViewModel()
            };

            _mockSecurityAnswerService.Setup(service => service.GetAllSecurityAnswers()).ReturnsAsync(expecteds);

            // Act
            var result = await _SecurityAnswerController.GetAllSecurityAnswers("") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var SecurityAnswerList = Assert.IsType<List<SecurityAnswerViewModel>>(result.Value);
            Assert.Equal(2, SecurityAnswerList.Count);
        }

        [Fact]
        public async Task SecurityAnswer_Controller_Save_SecurityAnswer_Test()
        {
            // Arrange
            var input = new SecurityAnswerAddModel();

            var expected = new SecurityAnswerViewModel();

            _mockSecurityAnswerService.Setup(service => service.SaveSecurityAnswer(input)).ReturnsAsync(expected);

            // Act
            var result = await _SecurityAnswerController.SaveSecurityAnswer(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<SecurityAnswerViewModel>(result.Value);
        }

        [Fact]
        public async Task SecurityAnswer_Controller_Update_SecurityAnswer_Test()
        {
            // Arrange
            Guid SecurityAnswerId = Guid.NewGuid();
            var input = new SecurityAnswerUpdateModel();

            var expected = true;

            _mockSecurityAnswerService.Setup(service => service.UpdateSecurityAnswer(input, SecurityAnswerId)).ReturnsAsync(true);

            // Act
            var result = await _SecurityAnswerController.UpdateSecurityAnswer(input, SecurityAnswerId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task SecurityAnswer_Controller_Delete_SecurityAnswer_Test()
        {
            // Arrange
            Guid SecurityAnswerId = Guid.NewGuid();
            var expected = true;

            _mockSecurityAnswerService.Setup(service => service.DeleteSecurityAnswer(SecurityAnswerId)).ReturnsAsync(true);

            // Act
            var result = await _SecurityAnswerController.DeleteSecurityAnswer(SecurityAnswerId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
