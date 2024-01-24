using InsternShip.Api.Controllers;
using InsternShip.Data.ViewModels.Skill;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InsternShip.UnitTest.Skill
{
    public class SkillControllerTests
    {
        private readonly Mock<ISkillService> _mockSkillService;
        private readonly SkillController _skillController;

        public SkillControllerTests()
        {
            _mockSkillService = new Mock<ISkillService>();
            _skillController = new SkillController(_mockSkillService.Object);
        }

        [Fact]
        public async Task Skill_Controller_Get_All_Test()
        {
            // Arrange
            string query = null!;
            var expecteds = new List<SkillViewModel>
            {
                new SkillViewModel
                {
                    SkillId = Guid.NewGuid(),
                    SkillName= "OpenCV",
                    Description= "OpenCV is a library of programming functions mainly for real-time computer vision.",
                    IsDeleted= false
                },
                new SkillViewModel
                {
                    SkillId= Guid.NewGuid(),
                    SkillName= "Kotlin",
                    Description= "Kotlin is a cross-platform, statically typed, general-purpose high-level programming language with type inference.",
                    IsDeleted= false
                }
            };

            _mockSkillService.Setup(service => service.GetAllSkills(query)).ReturnsAsync(expecteds);

            // Act
            var result = await _skillController.GetAllSkill(query) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var skillList = Assert.IsType<List<SkillViewModel>>(result.Value);
            Assert.Equal(2, skillList.Count);
        }

        [Fact]
        public async Task Skill_Controller_Save_Skill_Test()
        {
            // Arrange
            var input = new SkillAddModel
            {
                SkillName = "Nextjs",
                Description = "Next.js is an open-source web development framework created by the private company Vercel providing React-based web applications with server-side rendering and static website generation.",
            };

            var expected = new SkillViewModel
            {
                SkillName = "Nextjs",
                Description = "Next.js is an open-source web development framework created by the private company Vercel providing React-based web applications with server-side rendering and static website generation.",
                IsDeleted = false
            };

            _mockSkillService.Setup(service => service.SaveSkill(input)).ReturnsAsync(expected);

            // Act
            var result = await _skillController.SaveSkill(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<SkillViewModel>(result.Value);
            Assert.Equal(expected.SkillName, created.SkillName);
            Assert.Equal(expected.Description, created.Description);
            Assert.Equal(expected.IsDeleted, created.IsDeleted);
        }

        [Fact]
        public async Task Skill_Controller_Update_Skill_Test()
        {
            // Arrange
            Guid skillId = Guid.NewGuid();
            var input = new SkillUpdateModel();

            var expected = true;

            _mockSkillService.Setup(service => service.UpdateSkill(input, skillId)).ReturnsAsync(true);

            // Act
            var result = await _skillController.UpdateSkill(input, skillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Skill_Controller_Delete_Skill_Test()
        {
            // Arrange
            Guid skillId = Guid.NewGuid();
            var expected = true;

            _mockSkillService.Setup(service => service.DeleteSkill(skillId)).ReturnsAsync(true);

            // Act
            var result = await _skillController.DeleteSkill(skillId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted); // Add other property assertions
        }
    }
}
