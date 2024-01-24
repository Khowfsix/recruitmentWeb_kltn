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
using InsternShip.Data.ViewModels.Candidate;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class CandidateRepository_UnitTest
    {
        private readonly CandidateRepository _CandidateRepository;
        private readonly CandidateService _CandidateService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICandidateRepository _fakeCandidateRepository = A.Fake<ICandidateRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public CandidateRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _CandidateRepository = new CandidateRepository(_fakeDbContext, _fakeUow, _mapper);
            _CandidateService = new CandidateService(_fakeCandidateRepository, _mapper);
        }

        [Fact]
        public async Task Add_Candidate_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeCandidateId = Guid.NewGuid();

            var fakeCreatedCandidate = new CandidateAddModel{
                Experience = "string",
            };

            var mappedCreatedCandidate =_mapper.Map<CandidateModel>(fakeCreatedCandidate);
            mappedCreatedCandidate.CandidateId = fakeCandidateId;
            //Act
            var response = await _CandidateRepository.SaveCandidate(mappedCreatedCandidate);

            //Assert
            Assert.NotEqual(mappedCreatedCandidate.CandidateId, response.CandidateId);  
        }   

        [Fact]
        public async Task Get_Candidate_Returns_Correctly(){
            
            //Arrange
            List<CandidateModel> list = new();
            var expectedCreatedCandidate1 = new CandidateModel{
                CandidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedCandidate2 = new CandidateModel{
                CandidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedCandidate3 = new CandidateModel{
                CandidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            list.Add(expectedCreatedCandidate1);
            list.Add(expectedCreatedCandidate2);
            list.Add(expectedCreatedCandidate3);

            //Act
            A.CallTo(() => _fakeCandidateRepository.GetAllCandidates()).Returns(list);
            var response = await _CandidateService.GetAllCandidates();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<CandidateViewModel>>(response);
        }
    }
}

