using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.CategoryQuestion;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.CategoryQuestion
{
    public class CategoryQuestionControllerTests
    {
        private readonly Mock<ICategoryQuestionService> _mockCategoryQuestionService;
        private readonly CategoryQuestionController _CategoryQuestionController;

        public CategoryQuestionControllerTests()
        {
            _mockCategoryQuestionService = new Mock<ICategoryQuestionService>();
            _CategoryQuestionController = new CategoryQuestionController(_mockCategoryQuestionService.Object);
        }

        [Fact]
        public async Task CategoryQuestion_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CategoryQuestionViewModel>
            {
                new CategoryQuestionViewModel(),
                new CategoryQuestionViewModel()
            };

            _mockCategoryQuestionService.Setup(service => service.GetAllCategoryQuestions()).ReturnsAsync(expecteds);

            // Act
            var result = await _CategoryQuestionController.GetAllCategoryQuestions(null, null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var CategoryQuestionList = Assert.IsType<List<CategoryQuestionViewModel>>(result.Value);
            Assert.Equal(2, CategoryQuestionList.Count);
        }

        [Fact]
        public async Task CategoryQuestion_Controller_Save_CategoryQuestion_Test()
        {
            // Arrange
            var input = new CategoryQuestionAddModel();

            var expected = new CategoryQuestionViewModel();

            _mockCategoryQuestionService.Setup(service => service.SaveCategoryQuestion(input)).ReturnsAsync(expected);

            // Act
            var result = await _CategoryQuestionController.SaveCategoryQuestion(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CategoryQuestionViewModel>(result.Value);
        }

        [Fact]
        public async Task CategoryQuestion_Controller_Update_CategoryQuestion_Test()
        {
            // Arrange
            Guid CategoryQuestionId = Guid.NewGuid();
            var input = new CategoryQuestionUpdateModel();

            var expected = true;

            _mockCategoryQuestionService.Setup(service => service.UpdateCategoryQuestion(input, CategoryQuestionId)).ReturnsAsync(true);

            // Act
            var result = await _CategoryQuestionController.UpdateCategoryQuestion(input, CategoryQuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task CategoryQuestion_Controller_Delete_CategoryQuestion_Test()
        {
            // Arrange
            Guid CategoryQuestionId = Guid.NewGuid();
            var expected = true;

            _mockCategoryQuestionService.Setup(service => service.DeleteCategoryQuestion(CategoryQuestionId)).ReturnsAsync(true);

            // Act
            var result = await _CategoryQuestionController.DeleteCategoryQuestion(CategoryQuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
