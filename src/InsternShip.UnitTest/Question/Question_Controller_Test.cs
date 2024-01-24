using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Question;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Question
{
    public class QuestionControllerTests
    {
        private readonly Mock<IQuestionService> _mockQuestionService;
        private readonly QuestionController _QuestionController;

        public QuestionControllerTests()
        {
            _mockQuestionService = new Mock<IQuestionService>();
            _QuestionController = new QuestionController(_mockQuestionService.Object);
        }

        [Fact]
        public async Task Question_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<QuestionViewModel>
            {
                new QuestionViewModel(),
                new QuestionViewModel()
            };

            _mockQuestionService.Setup(service => service.GetAllQuestions(null, null)).ReturnsAsync(expecteds);

            // Act
            var result = await _QuestionController.GetAllQuestions(null, null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var QuestionList = Assert.IsType<List<QuestionViewModel>>(result.Value);
            Assert.Equal(2, QuestionList.Count);
        }

        [Fact]
        public async Task Question_Controller_Save_Question_Test()
        {
            // Arrange
            var input = new QuestionAddModel();

            var expected = new QuestionViewModel();

            _mockQuestionService.Setup(service => service.AddQuestion(input)).ReturnsAsync(expected);

            // Act
            var result = await _QuestionController.AddQuestion(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<QuestionViewModel>(result.Value);
        }

        [Fact]
        public async Task Question_Controller_Update_Question_Test()
        {
            // Arrange
            Guid QuestionId = Guid.NewGuid();
            var input = new QuestionUpdateModel();

            var expected = true;

            _mockQuestionService.Setup(service => service.UpdateQuestion(input, QuestionId)).ReturnsAsync(true);

            // Act
            var result = await _QuestionController.UpdateQuestion(input, QuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Question_Controller_Delete_Question_Test()
        {
            // Arrange
            Guid QuestionId = Guid.NewGuid();
            var expected = true;

            _mockQuestionService.Setup(service => service.RemoveQuestion(QuestionId)).ReturnsAsync(true);

            // Act
            var result = await _QuestionController.RemoveQuestion(QuestionId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
