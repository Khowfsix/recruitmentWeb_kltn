using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.QuestionSkill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.QuestionSkill
{
    public class QuestionSkillControllerTests
    {
        private readonly Mock<IQuestionSkillService> _mockQuestionSkillService;
        private readonly QuestionSkillController _QuestionSkillController;

        public QuestionSkillControllerTests()
        {
            _mockQuestionSkillService = new Mock<IQuestionSkillService>();
            _QuestionSkillController = new QuestionSkillController(_mockQuestionSkillService.Object);
        }

        [Fact]
        public async Task QuestionSkill_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<QuestionSkillViewModel>
            {
                new QuestionSkillViewModel(),
                new QuestionSkillViewModel()
            };

            _mockQuestionSkillService.Setup(service => service.GetAllQuestionSkills()).ReturnsAsync(expecteds);

            // Act
            var result = await _QuestionSkillController.GetAllQuestionSkills() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var QuestionSkillList = Assert.IsType<List<QuestionSkillViewModel>>(result.Value);
            Assert.Equal(2, QuestionSkillList.Count);
        }

        [Fact]
        public async Task QuestionSkill_Controller_Save_QuestionSkill_Test()
        {
            // Arrange
            var input = new QuestionSkillAddModel();

            var expected = new QuestionSkillViewModel();

            _mockQuestionSkillService.Setup(service => service.AddQuestionSkill(input)).ReturnsAsync(expected);

            // Act
            var result = await _QuestionSkillController.AddQuestionSkill(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<QuestionSkillViewModel>(result.Value);
        }

        [Fact]
        public async Task QuestionSkill_Controller_Update_QuestionSkill_Test()
        {
            // Arrange
            Guid QuestionSkillId = Guid.NewGuid();
            var input = new QuestionSkillUpdateModel();

            var expected = true;

            _mockQuestionSkillService.Setup(service => service.UpdateQuestionSkill(input, QuestionSkillId)).ReturnsAsync(true);

            // Act
            var result = await _QuestionSkillController.UpdateQuestionSkill(input, QuestionSkillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task QuestionSkill_Controller_Delete_QuestionSkill_Test()
        {
            // Arrange
            Guid QuestionSkillId = Guid.NewGuid();
            var expected = true;

            _mockQuestionSkillService.Setup(service => service.RemoveQuestionSkill(QuestionSkillId)).ReturnsAsync(true);

            // Act
            var result = await _QuestionSkillController.RemoveQuestionSkill(QuestionSkillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
