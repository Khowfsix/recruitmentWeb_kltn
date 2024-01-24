using InsternShip.Data;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.Repositories;
using FakeItEasy;
using Microsoft.EntityFrameworkCore.InMemory;
using InsternShip.Service;
using InsternShip.Service.Interfaces;
using InsternShip.Data.Entities;
using AutoMapper;
using InsternShip.Data.ViewModels.Event;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class EventRepository_UnitTest
    {
        private readonly EventRepository _EventRepository;
        private readonly EventService _EventService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IEventRepository _fakeEventRepository = A.Fake<IEventRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public EventRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _EventRepository = new EventRepository(_fakeDbContext, _fakeUow, _mapper);
            _EventService = new EventService(_fakeEventRepository, _mapper);
        }

        [Fact]
        public async Task Add_Event_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeEventId = Guid.NewGuid();

            var fakeCreatedEvent = new EventAddModel{
                EventName = "string",
            };

            var mappedCreatedEvent =_mapper.Map<EventModel>(fakeCreatedEvent);
            mappedCreatedEvent.EventId = fakeEventId;
            //Act
            var response = await _EventRepository.SaveEvent(mappedCreatedEvent);

            //Assert
            Assert.NotEqual(mappedCreatedEvent.EventId, response.EventId);
        }   

        [Fact]
        public async Task Get_Event_Returns_Correctly(){
            
            //Arrange
            List<EventModel> list = new();
            var expectedCreatedEvent1 = new EventModel{
                EventId = Guid.NewGuid(),
                EventName = "string",
                IsDeleted = false
            };
            var expectedCreatedEvent2 = new EventModel{
                EventId = Guid.NewGuid(),
                EventName = "string",
                IsDeleted = false
            };
            var expectedCreatedEvent3 = new EventModel{
                EventId = Guid.NewGuid(),
                EventName = "string",
                IsDeleted = false
            };
            list.Add(expectedCreatedEvent1);
            list.Add(expectedCreatedEvent2);
            list.Add(expectedCreatedEvent3);

            //Act
            A.CallTo(() => _fakeEventRepository.GetAllEvent()).Returns(list);
            var response = await _EventService.GetAllEvent();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<EventViewModel>>(response);
        }
    }
}

