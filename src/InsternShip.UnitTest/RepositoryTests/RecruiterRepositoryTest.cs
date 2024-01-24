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
using InsternShip.Data.ViewModels.Recruiter;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class RecruiterRepository_UnitTest
    {
        private readonly RecruiterRepository _RecruiterRepository;
        private readonly RecruiterService _recruiterService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IRecruiterRepository _fakeRecruiterRepository = A.Fake<IRecruiterRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public RecruiterRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _RecruiterRepository = new RecruiterRepository(_fakeDbContext, _fakeUow, _mapper);
            _recruiterService = new RecruiterService(_fakeRecruiterRepository, _mapper);
        }

        [Fact]
        public async Task Add_Recruiter_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeRecruiterId = Guid.NewGuid();

            var fakeCreatedRecruiter = new RecruiterAddModel{
                UserId = Guid.NewGuid().ToString(),
            };

            var mappedCreatedRecruiter =_mapper.Map<RecruiterModel>(fakeCreatedRecruiter);
            mappedCreatedRecruiter.RecruiterId = fakeRecruiterId;
            //Act
            var response = await _RecruiterRepository.SaveRecruiter(mappedCreatedRecruiter);

            //Assert
            Assert.NotEqual(mappedCreatedRecruiter.RecruiterId, response.RecruiterId);
        }   

        [Fact]
        public async Task Get_Recruiter_Returns_Correctly(){
            
            //Arrange
            List<RecruiterModel> list = new();
            var expectedCreatedRecruiter1 = new RecruiterModel{
                RecruiterId = Guid.NewGuid(),
                IsDeleted = false,
            };
            var expectedCreatedRecruiter2 = new RecruiterModel{
                RecruiterId = Guid.NewGuid(),
                IsDeleted = false,
            };
            var expectedCreatedRecruiter3 = new RecruiterModel{
                RecruiterId = Guid.NewGuid(),
                IsDeleted = false,
            };
            list.Add(expectedCreatedRecruiter1);
            list.Add(expectedCreatedRecruiter2);
            list.Add(expectedCreatedRecruiter3);

            //Act
            A.CallTo(() => _fakeRecruiterRepository.GetAllRecruiter()).Returns(list);
            var response = await _recruiterService.GetAllRecruiter();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<RecruiterViewModel>>(response);
        }
    }
}

