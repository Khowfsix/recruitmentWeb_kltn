using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.BlackList;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.BlackList
{
    public class BlackListControllerTests
    {
        private readonly Mock<IBlacklistService> _mockBlackListService;
        private readonly BlackListController _blackListController;

        public BlackListControllerTests()
        {
            _mockBlackListService = new Mock<IBlacklistService>();
            _blackListController = new BlackListController(_mockBlackListService.Object);
        }

        [Fact]
        public async Task BlackList_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<BlacklistViewModel>
            {
                new BlacklistViewModel(),
                new BlacklistViewModel()
            };

            _mockBlackListService.Setup(service => service.GetAllBlackLists()).ReturnsAsync(expecteds);

            // Act
            var result = await _blackListController.GetAllApplications() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var blackListList = Assert.IsType<List<BlacklistViewModel>>(result.Value);
            Assert.Equal(2, blackListList.Count);
        }

        [Fact]
        public async Task BlackList_Controller_Save_BlackList_Test()
        {
            // Arrange
            var input = new BlackListAddModel
            {
                CandidateId = Guid.NewGuid(),
                Reason = "Reason",
                DateTime = DateTime.Now
            };

            var expected = new BlacklistViewModel
            {
                BlackListId = Guid.NewGuid(),
                CandidateId = Guid.NewGuid(),
                Reason = "Reason",
                DateTime = DateTime.Now,
                Status = 1,
                IsDeleted = false

            };

            _mockBlackListService.Setup(service => service.SaveBlackList(input)).ReturnsAsync(expected);

            // Act
            var result = await _blackListController.SaveBlackList(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<BlacklistViewModel>(result.Value);
            Assert.Equal(expected.IsDeleted, created.IsDeleted);
        }

        [Fact]
        public async Task BlackList_Controller_Update_BlackList_Test()
        {
            // Arrange
            Guid blackListId = Guid.NewGuid();
            var input = new BlackListUpdateModel();

            var expected = true;

            _mockBlackListService.Setup(service => service.UpdateBlackList(input, blackListId)).ReturnsAsync(true);

            // Act
            var result = await _blackListController.UpdateBlackList(input, blackListId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task BlackList_Controller_Delete_BlackList_Test()
        {
            // Arrange
            Guid blackListId = Guid.NewGuid();
            var expected = true;

            _mockBlackListService.Setup(service => service.DeleteBlackList(blackListId)).ReturnsAsync(true);

            // Act
            var result = await _blackListController.DeleteBlackList(blackListId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted); // Add other property assertions
        }
    }
}
