using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.SuccessfulCadidate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.SuccessfulCadidate
{
    public class SuccessfulCadidateControllerTests
    {
        private readonly Mock<ISuccessfulCandidateService> _mockSuccessfulCadidateService;
        private readonly SuccessfulCadidateController _SuccessfulCadidateController;

        public SuccessfulCadidateControllerTests()
        {
            _mockSuccessfulCadidateService = new Mock<ISuccessfulCandidateService>();
            _SuccessfulCadidateController = new SuccessfulCadidateController(_mockSuccessfulCadidateService.Object);
        }

        [Fact]
        public async Task SuccessfulCadidate_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<SuccessfulCadidateViewModel>
            {
                new SuccessfulCadidateViewModel(),
                new SuccessfulCadidateViewModel()
            };

            _mockSuccessfulCadidateService.Setup(service => service.GetAllSuccessfulCadidates(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _SuccessfulCadidateController.GetAllSCs(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var SuccessfulCadidateList = Assert.IsType<List<SuccessfulCadidateViewModel>>(result.Value);
            Assert.Equal(2, SuccessfulCadidateList.Count);
        }

        [Fact]
        public async Task SuccessfulCadidate_Controller_Save_SuccessfulCadidate_Test()
        {
            // Arrange
            var input = new SuccessfulCadidateAddModel();

            var expected = new SuccessfulCadidateViewModel();

            _mockSuccessfulCadidateService.Setup(service => service.SaveSuccessfulCadidate(input)).ReturnsAsync(expected);

            // Act
            var result = await _SuccessfulCadidateController.SaveSC(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<SuccessfulCadidateViewModel>(result.Value);
        }

        [Fact]
        public async Task SuccessfulCadidate_Controller_Update_SuccessfulCadidate_Test()
        {
            // Arrange
            Guid SuccessfulCadidateId = Guid.NewGuid();
            var input = new SuccessfulCadidateUpdateModel();

            var expected = true;

            _mockSuccessfulCadidateService.Setup(service => service.UpdateSuccessfulCadidate(input, SuccessfulCadidateId)).ReturnsAsync(true);

            // Act
            var result = await _SuccessfulCadidateController.UpdateCS(input, SuccessfulCadidateId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task SuccessfulCadidate_Controller_Delete_SuccessfulCadidate_Test()
        {
            // Arrange
            Guid SuccessfulCadidateId = Guid.NewGuid();
            var expected = true;

            _mockSuccessfulCadidateService.Setup(service => service.DeleteSuccessfulCadidate(SuccessfulCadidateId)).ReturnsAsync(true);

            // Act
            var result = await _SuccessfulCadidateController.DeleteCS(SuccessfulCadidateId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
