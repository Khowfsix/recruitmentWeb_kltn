using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Certificate
{
    public class CertificateControllerTests
    {
        private readonly Mock<ICertificateService> _mockCertificateService;
        private readonly CertificateController _CertificateController;

        public CertificateControllerTests()
        {
            _mockCertificateService = new Mock<ICertificateService>();
            _CertificateController = new CertificateController(_mockCertificateService.Object);
        }

        [Fact]
        public async Task Certificate_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<CertificateViewModel>
            {
                new CertificateViewModel(),
                new CertificateViewModel()
            };

            _mockCertificateService.Setup(service => service.GetAllCertificate(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _CertificateController.GetAllCertificate(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var CertificateList = Assert.IsType<List<CertificateViewModel>>(result.Value);
            Assert.Equal(2, CertificateList.Count);
        }

        [Fact]
        public async Task Certificate_Controller_Save_Certificate_Test()
        {
            // Arrange
            var input = new CertificateAddModel();

            var expected = new CertificateViewModel();

            _mockCertificateService.Setup(service => service.SaveCertificate(input)).ReturnsAsync(expected);

            // Act
            var result = await _CertificateController.SaveCertificate(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<CertificateViewModel>(result.Value);
        }

        [Fact]
        public async Task Certificate_Controller_Update_Certificate_Test()
        {
            // Arrange
            Guid CertificateId = Guid.NewGuid();
            var input = new CertificateUpdateModel();

            var expected = true;

            _mockCertificateService.Setup(service => service.UpdateCertificate(input, CertificateId)).ReturnsAsync(true);

            // Act
            var result = await _CertificateController.UpdateCertificate(input, CertificateId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Certificate_Controller_Delete_Certificate_Test()
        {
            // Arrange
            Guid CertificateId = Guid.NewGuid();
            var expected = true;

            _mockCertificateService.Setup(service => service.DeleteCertificate(CertificateId)).ReturnsAsync(true);

            // Act
            var result = await _CertificateController.DeleteCertificate(CertificateId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
