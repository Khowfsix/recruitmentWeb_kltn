using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Shift;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Shift
{
    public class ShiftControllerTests
    {
        private readonly Mock<IShiftService> _mockShiftService;
        private readonly ShiftController _ShiftController;

        public ShiftControllerTests()
        {
            _mockShiftService = new Mock<IShiftService>();
            _ShiftController = new ShiftController(_mockShiftService.Object);
        }

        [Fact]
        public async Task Shift_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<ShiftViewModel>
            {
                new ShiftViewModel(),
                new ShiftViewModel()
            };

            _mockShiftService.Setup(service => service.GetAllShifts(null)).ReturnsAsync(expecteds);

            // Act
            var result = await _ShiftController.GetAllShift(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var ShiftList = Assert.IsType<List<ShiftViewModel>>(result.Value);
            Assert.Equal(2, ShiftList.Count);
        }

        [Fact]
        public async Task Shift_Controller_Save_Shift_Test()
        {
            // Arrange
            var input = new ShiftAddModel();

            var expected = new ShiftViewModel();

            _mockShiftService.Setup(service => service.SaveShift(input)).ReturnsAsync(expected);

            // Act
            var result = await _ShiftController.SaveShift(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<ShiftViewModel>(result.Value);
        }

        [Fact]
        public async Task Shift_Controller_Update_Shift_Test()
        {
            // Arrange
            Guid ShiftId = Guid.NewGuid();
            var input = new ShiftUpdateModel();

            var expected = true;

            _mockShiftService.Setup(service => service.UpdateShift(input, ShiftId)).ReturnsAsync(true);

            // Act
            var result = await _ShiftController.UpdateShift(input, ShiftId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Shift_Controller_Delete_Shift_Test()
        {
            // Arrange
            Guid ShiftId = Guid.NewGuid();
            var expected = true;

            _mockShiftService.Setup(service => service.DeleteShift(ShiftId)).ReturnsAsync(true);

            // Act
            var result = await _ShiftController.DeleteRound(ShiftId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
