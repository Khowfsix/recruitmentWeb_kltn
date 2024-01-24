using InsternShip.Api.Controllers;
using InsternShip.Data.Entities;
using InsternShip.Data.ViewModels.Event;
using InsternShip.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Drawing;

namespace InsternShip.UnitTest.Event
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _mockEventService;
        private readonly EventController _EventController;

        public EventControllerTests()
        {
            _mockEventService = new Mock<IEventService>();
            _EventController = new EventController(_mockEventService.Object);
        }

        [Fact]
        public async Task Event_Controller_Get_All_Test()
        {
            // Arrange
            var expecteds = new List<EventViewModel>
            {
                new EventViewModel(),
                new EventViewModel()
            };

            _mockEventService.Setup(service => service.GetAllEvent()).ReturnsAsync(expecteds);

            // Act
            var result = await _EventController.GetAllEvent(null) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var EventList = Assert.IsType<List<EventViewModel>>(result.Value);
            Assert.Equal(2, EventList.Count);
        }

        [Fact]
        public async Task Event_Controller_Save_Event_Test()
        {
            // Arrange
            var input = new EventAddModel();

            var expected = new EventViewModel();

            _mockEventService.Setup(service => service.SaveEvent(input)).ReturnsAsync(expected);

            // Act
            var result = await _EventController.SaveEvent(input) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var created = Assert.IsType<EventViewModel>(result.Value);
        }

        [Fact]
        public async Task Event_Controller_Update_Event_Test()
        {
            // Arrange
            Guid EventId = Guid.NewGuid();
            var input = new EventUpdateModel();

            var expected = true;

            _mockEventService.Setup(service => service.UpdateEvent(input, EventId)).ReturnsAsync(true);

            // Act
            var result = await _EventController.UpdateEvent(input, EventId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var updated = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, updated);
        }

        [Fact]
        public async Task Event_Controller_Delete_Event_Test()
        {
            // Arrange
            Guid EventId = Guid.NewGuid();
            var expected = true;

            _mockEventService.Setup(service => service.DeleteEvent(EventId)).ReturnsAsync(true);

            // Act
            var result = await _EventController.DeleteEvent(EventId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var deleted = Assert.IsType<bool>(result.Value);
            Assert.Equal(expected, deleted);
        }
    }
}
