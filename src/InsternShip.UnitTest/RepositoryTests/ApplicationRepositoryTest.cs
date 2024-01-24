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
using InsternShip.Data.ViewModels.Language;
using InsternShip.Data.Mapping;
using InsternShip.Data.ViewModels.Application;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class ApplicationRepository_UnitTest
    {
        private readonly ApplicationRepository _applicationRepository;
        private readonly ApplicationService _applicationService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICvRepository _fakeCvRepository = A.Fake<ICvRepository>();
        private readonly IBlacklistRepository _fakeBlacklistRepository = A.Fake<IBlacklistRepository>();
        private readonly IApplicationRepository _fakeApplicationRepository = A.Fake<IApplicationRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public ApplicationRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _applicationRepository = new ApplicationRepository(_fakeDbContext, _fakeUow, _mapper);

            _applicationService = new ApplicationService(_fakeApplicationRepository, _fakeCvRepository
                                                        ,_fakeBlacklistRepository, _mapper);
        }

        [Fact]
        public async Task Save_Application_In_Repository_Returns_Correctly()
        {
            //Arrange
            var fakeApplicationId = Guid.NewGuid();

            var fakeCreatedApplication = new ApplicationAddModel{
                Cvid = Guid.NewGuid(),
                PositionId = Guid.NewGuid(),
            };

            var mappedCreatedApplication =_mapper.Map<ApplicationModel>(fakeCreatedApplication);
            mappedCreatedApplication.ApplicationId = fakeApplicationId;
            //Act
            var response = await _applicationRepository.SaveApplication(mappedCreatedApplication);

            //Assert
            Assert.NotEqual(mappedCreatedApplication.ApplicationId, response.ApplicationId);
        }   

        [Fact]
        public async Task Get_Application_Returns_Correctly(){
            
            //Arrange
            var fakeApplicationId = Guid.NewGuid();
            var expectedCreatedApplication = new ApplicationModel{
                ApplicationId = fakeApplicationId,
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakeApplicationRepository.GetApplicationById(fakeApplicationId)).Returns(expectedCreatedApplication);
            var response = await _applicationService.GetApplicationById(fakeApplicationId);

            //Assert
            Assert.Equal(expectedCreatedApplication.ApplicationId, response.ApplicationId);
        }
    }
}

