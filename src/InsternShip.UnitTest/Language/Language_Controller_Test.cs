using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Language;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Language
{
    public class LanguageControllerTests
    {
        private readonly Mock<ILanguageService> _mockLanguageService;
        private readonly LanguageController _LanguageController;

        public LanguageControllerTests()
        {
            _mockLanguageService = new Mock<ILanguageService>();
            _LanguageController = new LanguageController(_mockLanguageService.Object);
        }

        [Fact]
        public async Task Language_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<LanguageViewModel>
            {
                new LanguageViewModel(),
                new LanguageViewModel()
            };

            _mockLanguageService.Setup(service => service.GetAllLanguages()).ReturnsAsync(expecteds);

            // Act
            var result = await _LanguageController.GetAllLanguages(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var LanguageList = Assert.IsType<List<LanguageViewModel>>(result.Value);
            Assert.Equal(2, LanguageList.Count);
        }

        [Fact]
        public async Task Language_Controller_Save_Language_Test()
        {
            // Arrange
            var input = new LanguageAddModel();

            var expected = new LanguageViewModel();

            _mockLanguageService.Setup(service => service.AddLanguage(input)).ReturnsAsync(expected);

            // Act
            var result = await _LanguageController.AddLanguage(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<LanguageViewModel>(result.Value);
        }

        [Fact]
        public async Task Language_Controller_Update_Language_Test()
        {
            // Arrange
            Guid LanguageId = Guid.NewGuid();
            var input = new LanguageUpdateModel();

            var expected = true;

            _mockLanguageService.Setup(service => service.UpdateLanguage(input, LanguageId)).ReturnsAsync(true);

            // Act
            var result = await _LanguageController.UpdateLanguage(input, LanguageId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Language_Controller_Delete_Language_Test()
        {
            // Arrange
            Guid LanguageId = Guid.NewGuid();
            var expected = true;

            _mockLanguageService.Setup(service => service.RemoveLanguage(LanguageId)).ReturnsAsync(true);

            // Act
            var result = await _LanguageController.DeleteLanguage(LanguageId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
