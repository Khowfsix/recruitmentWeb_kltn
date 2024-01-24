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
using InsternShip.Data.ViewModels.Position;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class PositionRepository_UnitTest
    {
        private readonly PositionRepository _PositionRepository;
        private readonly PositionService _PositionService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IPositionRepository _fakePositionRepository = A.Fake<IPositionRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public PositionRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _PositionRepository = new PositionRepository(_fakeDbContext, _fakeUow, _mapper);
            _PositionService = new PositionService(_fakePositionRepository, _mapper);
        }

        [Fact]
        public async Task Add_Position_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakePositionId = Guid.NewGuid();

            var fakeCreatedPosition = new PositionAddModel{
                PositionName = "string",
            };

            var mappedCreatedPosition =_mapper.Map<PositionModel>(fakeCreatedPosition);
            mappedCreatedPosition.PositionId = fakePositionId;
            //Act
            var response = await _PositionRepository.AddPosition(mappedCreatedPosition);

            //Assert
            Assert.Equal(fakeCreatedPosition.PositionName, response.PositionName);
            Assert.NotEqual(fakePositionId, response.PositionId);
        }   

        [Fact]
        public async Task Get_Position_Returns_Correctly(){
            
            //Arrange
            var fakePositionId = Guid.NewGuid();
            var expectedCreatedPosition = new PositionModel{
                PositionId = fakePositionId,
                PositionName = "string",
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakePositionRepository.GetPositionById(fakePositionId)).Returns(expectedCreatedPosition);
            var response = await _PositionService.GetPositionById(fakePositionId);

            //Assert
            Assert.Equal(expectedCreatedPosition.PositionId, response.PositionId);
        }
    }
}

