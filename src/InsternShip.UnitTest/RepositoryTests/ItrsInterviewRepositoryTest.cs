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
using InsternShip.Data.ViewModels.Itrsinterview;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class ItrsInterviewRepository_UnitTest
    {
        private readonly ItrsinterviewRepository _ItrsinterviewRepository;
        private readonly ItrsinterviewService _ItrsinterviewService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IInterviewRepository _fakeInterviewRepository = A.Fake<IInterviewRepository>();
        private readonly IItrsinterviewRepository _fakeItrsinterviewRepository = A.Fake<IItrsinterviewRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public ItrsInterviewRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _ItrsinterviewRepository = new ItrsinterviewRepository(_fakeDbContext, _fakeUow, _mapper);
            _ItrsinterviewService = new ItrsinterviewService(_fakeItrsinterviewRepository,
                                                 _fakeInterviewRepository, _mapper);
        }

        [Fact]
        public async Task Add_Itrsinterview_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeItrsinterviewId = Guid.NewGuid();

            var fakeCreatedItrsinterview = new ItrsinterviewAddModel{
            };

            var mappedCreatedItrsinterview =_mapper.Map<ItrsinterviewModel>(fakeCreatedItrsinterview);
            mappedCreatedItrsinterview.ItrsinterviewId = fakeItrsinterviewId;
            //Act
            var response = await _ItrsinterviewRepository.SaveItrsinterview(mappedCreatedItrsinterview, fakeItrsinterviewId);

            //Assert
            Assert.NotEqual(mappedCreatedItrsinterview.ItrsinterviewId, response.ItrsinterviewId);
        }   

        [Fact]
        public async Task Get_Itrsinterview_Returns_Correctly(){
            
            //Arrange
            var fakeItrsinterviewId = Guid.NewGuid();
            var expectedCreatedItrsinterview = new ItrsinterviewModel{
                ItrsinterviewId = fakeItrsinterviewId,
            };

            //Act
            A.CallTo(() => _fakeItrsinterviewRepository.GetItrsinterviewById(fakeItrsinterviewId)).Returns(expectedCreatedItrsinterview);
            var response = await _ItrsinterviewService.GetItrsinterviewById(fakeItrsinterviewId);

            //Assert
            Assert.Equal(expectedCreatedItrsinterview.ItrsinterviewId, response.ItrsinterviewId);
        }
    }
}

