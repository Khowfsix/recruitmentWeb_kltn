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
using InsternShip.Data.ViewModels.Cv;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class CvRepository_UnitTest
    {
        private readonly CvRepository _CvRepository;
        private readonly CvService _CvService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICandidateRepository _fakeCandidateRepository = A.Fake<ICandidateRepository>();
        private readonly ICvHasSkillrepository _fakeCvHasSkillRepository = A.Fake<ICvHasSkillrepository>();
        private readonly IUploadFileRepository _fakeUploadFileRepository = A.Fake<IUploadFileRepository>();
        private readonly IUploadFileService _fakeUploadFileService = A.Fake<IUploadFileService>();
        private readonly ICvRepository _fakeCvRepository = A.Fake<ICvRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly ICertificateRepository _fakeCertificateRepository = A.Fake<ICertificateRepository>();
        private readonly IMapper _mapper;
        public CvRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _CvRepository = new CvRepository(_fakeDbContext, _fakeUow, _mapper,
                                        _fakeCandidateRepository, _fakeUploadFileRepository);

            _CvService = new CvService(_fakeCvRepository, _fakeCvHasSkillRepository,
                                        _fakeCandidateRepository, _fakeCertificateRepository,
                                        _mapper, _fakeUploadFileService);
        }

        [Fact]
        public async Task Add_Cv_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeCvid = Guid.NewGuid();

            var fakeCreatedCv = new CvAddModel{
                CvName = "string",
            };

            var mappedCreatedCv =_mapper.Map<CvModel>(fakeCreatedCv);
            mappedCreatedCv.Cvid = fakeCvid;
            //Act
            var response = await _CvRepository.SaveCv(mappedCreatedCv);

            //Assert
            Assert.Equal(fakeCreatedCv.CvName, response.Item2.CvName);
            Assert.NotEqual(fakeCvid, response.Item2.Cvid);
        }   

        [Fact]
        public async Task Get_Cv_Returns_Correctly(){
            
            //Arrange
            var fakeCvid = Guid.NewGuid();
            var expectedCreatedCv = new CvModel{
                Cvid = fakeCvid,
                CvName = "string",
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakeCvRepository.GetCVById(fakeCvid)).Returns(expectedCreatedCv);
            
            var response = await _CvService.GetCvById(fakeCvid);

            //Assert
            Assert.Equal(expectedCreatedCv.Cvid, response.Cvid);
        }
    }
}

