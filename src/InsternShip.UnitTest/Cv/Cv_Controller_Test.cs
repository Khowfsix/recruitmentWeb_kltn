using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Cv;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Cv
{
    public class CvControllerTests
    {
        private readonly Mock<ICvService> _mockCvService;
        private readonly CvController _CvController;

        public CvControllerTests()
        {
            _mockCvService = new Mock<ICvService>();
            _CvController = new CvController(_mockCvService.Object, null!, null!, null!, null!);
        }

        [Fact]
        public async Task Cv_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CvViewModel>
            {
                new CvViewModel(),
                new CvViewModel()
            };

            _mockCvService.Setup(service => service.GetAllCv(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _CvController.GetAllCv(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var CvList = Assert.IsType<List<CvViewModel>>(result.Value);
            Assert.Equal(2, CvList.Count);
        }

        [Fact]
        public async Task Cv_Controller_Save_Cv_Test()
        {
            // Arrange
            var input = new CvAddModel();

            var expected = new CvViewModel();

            _mockCvService.Setup(service => service.SaveCv(input)).ReturnsAsync(expected);

            // Act
            var result = await _CvController.SaveCv(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CvViewModel>(result.Value);
        }

        [Fact]
        public async Task Cv_Controller_Update_Cv_Test()
        {
            // Arrange
            Guid CvId = Guid.NewGuid();
            var input = new CvUpdateModel();

            var expected = true;

            _mockCvService.Setup(service => service.UpdateCv(input, CvId)).ReturnsAsync(true);

            // Act
            var result = await _CvController.UpdateCv(input, CvId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Cv_Controller_Delete_Cv_Test()
        {
            // Arrange
            Guid CvId = Guid.NewGuid();
            var expected = true;

            _mockCvService.Setup(service => service.DeleteCv(CvId)).ReturnsAsync(true);

            // Act
            var result = await _CvController.DeleteCv(CvId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
