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
using InsternShip.Data.ViewModels.SuccessfulCadidate;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class SuccessfulCandidateRepository_UnitTest
    {
        private readonly SuccessfulCadidateRepository _SuccessfulCadidateRepository;
        private readonly SuccessfulCandidateService _SuccessfulCadidateService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ISuccessfulCadidateRepository _fakeSuccessfulCadidateRepository = A.Fake<ISuccessfulCadidateRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public SuccessfulCandidateRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _SuccessfulCadidateRepository = new SuccessfulCadidateRepository(_fakeDbContext, _fakeUow, _mapper);
            _SuccessfulCadidateService = new SuccessfulCandidateService(_fakeSuccessfulCadidateRepository, _mapper);
        }

        [Fact]
        public async Task Add_SuccessfulCadidate_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeSuccessfulCadidateId = Guid.NewGuid();

            var fakeCreatedSuccessfulCadidate = new SuccessfulCadidateAddModel{
            };

            var mappedCreatedSuccessfulCadidate =_mapper.Map<SuccessfulCadidateModel>(fakeCreatedSuccessfulCadidate);
            mappedCreatedSuccessfulCadidate.SuccessfulCadidateId = fakeSuccessfulCadidateId;
            //Act
            var response = await _SuccessfulCadidateRepository.SaveSuccessfulCadidate(mappedCreatedSuccessfulCadidate);

            //Assert
            Assert.NotEqual(mappedCreatedSuccessfulCadidate.SuccessfulCadidateId, response.SuccessfulCadidateId);
        }   

        [Fact]
        public async Task Get_SuccessfulCadidate_Returns_Correctly(){
            
            //Arrange
            List<SuccessfulCadidateModel> list = new();
            var expectedCreatedSuccessfulCadidate1 = new SuccessfulCadidateModel{
                SuccessfulCadidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedSuccessfulCadidate2 = new SuccessfulCadidateModel{
                SuccessfulCadidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedSuccessfulCadidate3 = new SuccessfulCadidateModel{
                SuccessfulCadidateId = Guid.NewGuid(),
                IsDeleted = false
            };
            list.Add(expectedCreatedSuccessfulCadidate3);
            list.Add(expectedCreatedSuccessfulCadidate2);
            list.Add(expectedCreatedSuccessfulCadidate3);

            //Act
            A.CallTo(() => _fakeSuccessfulCadidateRepository.GetAllSuccessfulCadidates(null)).Returns(list);
            var response = await _SuccessfulCadidateService.GetAllSuccessfulCadidates(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<SuccessfulCadidateViewModel>>(response);
        }
    }
}

