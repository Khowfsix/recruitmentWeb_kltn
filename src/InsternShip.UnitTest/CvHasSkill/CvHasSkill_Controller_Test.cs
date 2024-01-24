using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.CvHasSkill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.CvHasSkill
{
    public class CvHasSkillControllerTests
    {
        private readonly Mock<ICvHasSkillService> _mockCvHasSkillService;
        private readonly CvHasSkillController _CvHasSkillController;

        public CvHasSkillControllerTests()
        {
            _mockCvHasSkillService = new Mock<ICvHasSkillService>();
            _CvHasSkillController = new CvHasSkillController(_mockCvHasSkillService.Object);
        }

        [Fact]
        public async Task CvHasSkill_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CvHasSkillViewModel>
            {
                new CvHasSkillViewModel(),
                new CvHasSkillViewModel()
            };

            _mockCvHasSkillService.Setup(service => service.GetAllCvHasSkillService(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _CvHasSkillController.GetAllCvHasSkill(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var CvHasSkillList = Assert.IsType<List<CvHasSkillViewModel>>(result.Value);
            Assert.Equal(2, CvHasSkillList.Count);
        }

        [Fact]
        public async Task CvHasSkill_Controller_Save_CvHasSkill_Test()
        {
            // Arrange
            var input = new CvHasSkillAddModel();

            var expected = new CvHasSkillViewModel();

            _mockCvHasSkillService.Setup(service => service.SaveCvHasSkillService(input)).ReturnsAsync(expected);

            // Act
            var result = await _CvHasSkillController.SaveCvHasSkill(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CvHasSkillViewModel>(result.Value);
        }

        [Fact]
        public async Task CvHasSkill_Controller_Update_CvHasSkill_Test()
        {
            // Arrange
            Guid CvHasSkillId = Guid.NewGuid();
            var input = new CvHasSkillUpdateModel();

            var expected = true;

            _mockCvHasSkillService.Setup(service => service.UpdateCvHasSkillService(input, CvHasSkillId)).ReturnsAsync(true);

            // Act
            var result = await _CvHasSkillController.UpdateCvHasSkill(input, CvHasSkillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task CvHasSkill_Controller_Delete_CvHasSkill_Test()
        {
            // Arrange
            Guid CvHasSkillId = Guid.NewGuid();
            var expected = true;

            _mockCvHasSkillService.Setup(service => service.DeleteCvHasSkillService(CvHasSkillId)).ReturnsAsync(true);

            // Act
            var result = await _CvHasSkillController.DeleteCvHasSkill(CvHasSkillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
