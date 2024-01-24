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

namespace InsternShip.UnitTest.RepositoryTests
{
    public class LanguageRepository_UnitTest
    {
        private readonly LanguageRepository _languageRepository;
        private readonly LanguageService _languageService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ILanguageRepository _fakeLanguageRepository = A.Fake<ILanguageRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public LanguageRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _languageRepository = new LanguageRepository(_fakeDbContext, _fakeUow, _mapper);
            _languageService = new LanguageService(_fakeLanguageRepository, _mapper);
        }

        [Fact]
        public async Task Add_Language_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeLanguageId = Guid.NewGuid();

            var fakeCreatedLanguage = new LanguageAddModel{
                LanguageName = "string",
            };

            var mappedCreatedLanguage =_mapper.Map<LanguageModel>(fakeCreatedLanguage);
            mappedCreatedLanguage.LanguageId = fakeLanguageId;
            //Act
            var response = await _languageRepository.AddLanguage(mappedCreatedLanguage);

            //Assert
            Assert.Equal(fakeCreatedLanguage.LanguageName, response.LanguageName);
            Assert.NotEqual(fakeLanguageId, response.LanguageId);
        }   

        [Fact]
        public async Task Get_Language_Returns_Correctly(){
            
            //Arrange
            var fakeLanguageId = Guid.NewGuid();
            var expectedCreatedLanguage = new LanguageModel{
                LanguageId = fakeLanguageId,
                LanguageName = "string",
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakeLanguageRepository.GetLanguage(fakeLanguageId)).Returns(expectedCreatedLanguage);
            var response = await _languageService.GetLanguage(fakeLanguageId);

            //Assert
            Assert.Equal(expectedCreatedLanguage.LanguageId, response.LanguageId);
        }
    }
}

