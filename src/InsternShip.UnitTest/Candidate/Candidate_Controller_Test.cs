using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Candidate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Candidate
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidateController _candidateController;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _candidateController = new CandidateController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task Candidate_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CandidateViewModel>
            {
                new CandidateViewModel(),
                new CandidateViewModel()
            };

            _mockCandidateService.Setup(service => service.GetAllCandidates()).ReturnsAsync(expecteds);

            // Act
            var result = await _candidateController.GetAllCandidates() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var candidateList = Assert.IsType<List<CandidateViewModel>>(result.Value);
            Assert.Equal(2, candidateList.Count);
        }

        [Fact]
        public async Task Candidate_Controller_Save_Candidate_Test()
        {
            // Arrange
            var input = new CandidateAddModel();

            var expected = new CandidateViewModel();

            _mockCandidateService.Setup(service => service.SaveCandidate(input)).ReturnsAsync(expected);

            // Act
            var result = await _candidateController.SaveCandidate(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CandidateViewModel>(result.Value);
        }

        [Fact]
        public async Task Candidate_Controller_Update_Candidate_Test()
        {
            // Arrange
            var input = new CandidateUpdateModel();

            var expected = true;

            _mockCandidateService.Setup(service => service.UpdateCandidate(input, Guid.Empty)).ReturnsAsync(true);

            // Act
            var result = await _candidateController.UpdateCandidate(input, Guid.Empty) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Candidate_Controller_Delete_Candidate_Test()
        {
            // Arrange
            Guid candidateId = Guid.NewGuid();
            var expected = true;

            _mockCandidateService.Setup(service => service.DeleteCandidate(candidateId)).ReturnsAsync(true);

            // Act
            var result = await _candidateController.DeleteCandidate(candidateId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
