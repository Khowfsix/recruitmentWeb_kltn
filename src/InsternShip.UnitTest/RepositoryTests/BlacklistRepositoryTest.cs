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
using InsternShip.Data.Mapping;
using InsternShip.Data.ViewModels.BlackList;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class BlacklistRepository_UnitTest
    {
        private readonly BlackListRepository _BlacklistRepository;
        private readonly BlacklistService _BlacklistService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IBlacklistRepository _fakeBlacklistRepository = A.Fake<IBlacklistRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public BlacklistRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _BlacklistRepository = new BlackListRepository(_fakeDbContext, _fakeUow, _mapper);
            _BlacklistService = new BlacklistService(_fakeBlacklistRepository, _mapper);
        }

        [Fact]
        public async Task Add_Blacklist_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeBlacklistId = Guid.NewGuid();
            var fakeCreatedBlacklist = new BlackListAddModel{
                Reason = "string",
            };

            var mappedCreatedBlacklist =_mapper.Map<BlacklistModel>(fakeCreatedBlacklist);
            mappedCreatedBlacklist.BlackListId = fakeBlacklistId;
            //Act
            var response = await _BlacklistRepository.SaveBlackList(mappedCreatedBlacklist);

            //Assert
            Assert.NotEqual(mappedCreatedBlacklist.BlackListId, response.BlackListId);
        }   

        [Fact]
        public async Task Get_Blacklist_Returns_Correctly(){
            
            //Arrange
            

            //
            List<BlacklistModel> blacklistModels = new();
            
            var expectedCreatedBlacklist1 = new BlacklistModel{
                Reason = "string",
            };
            var expectedCreatedBlacklist2 = new BlacklistModel{
                Reason = "string",
            };
            var expectedCreatedBlacklist3 = new BlacklistModel{
                Reason = "string",
            };

            blacklistModels.Add(expectedCreatedBlacklist1); 
            blacklistModels.Add(expectedCreatedBlacklist2);
            blacklistModels.Add(expectedCreatedBlacklist3);

            //Act
            A.CallTo(() => _fakeBlacklistRepository.GetAllBlackLists()).Returns(blacklistModels);
            var response = await _BlacklistService.GetAllBlackLists();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<BlacklistViewModel>>(response);
        }
    }
}

