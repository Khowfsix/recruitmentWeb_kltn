using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Department;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Department
{
    public class DepartmentControllerTests
    {
        private readonly Mock<IDepartmentService> _mockDepartmentService;
        private readonly DepartmentController _DepartmentController;

        public DepartmentControllerTests()
        {
            _mockDepartmentService = new Mock<IDepartmentService>();
            _DepartmentController = new DepartmentController(_mockDepartmentService.Object);
        }

        [Fact]
        public async Task Department_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<DepartmentViewModel>
            {
                new DepartmentViewModel(),
                new DepartmentViewModel()
            };

            _mockDepartmentService.Setup(service => service.GetAllDepartment(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _DepartmentController.GetAllDepartment(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var DepartmentList = Assert.IsType<List<DepartmentViewModel>>(result.Value);
            Assert.Equal(2, DepartmentList.Count);
        }

        [Fact]
        public async Task Department_Controller_Save_Department_Test()
        {
            // Arrange
            var input = new DepartmentAddModel();

            var expected = new DepartmentViewModel();

            _mockDepartmentService.Setup(service => service.SaveDepartment(input)).ReturnsAsync(expected);

            // Act
            var result = await _DepartmentController.SaveDepartment(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<DepartmentViewModel>(result.Value);
        }

        [Fact]
        public async Task Department_Controller_Update_Department_Test()
        {
            // Arrange
            Guid DepartmentId = Guid.NewGuid();
            var input = new DepartmentUpdateModel();

            var expected = true;

            _mockDepartmentService.Setup(service => service.UpdateDepartment(input, DepartmentId)).ReturnsAsync(true);

            // Act
            var result = await _DepartmentController.UpdateDepartment(input, DepartmentId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Department_Controller_Delete_Department_Test()
        {
            // Arrange
            Guid DepartmentId = Guid.NewGuid();
            var expected = true;

            _mockDepartmentService.Setup(service => service.DeleteDepartment(DepartmentId)).ReturnsAsync(true);

            // Act
            var result = await _DepartmentController.DeleteDepartment(DepartmentId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
