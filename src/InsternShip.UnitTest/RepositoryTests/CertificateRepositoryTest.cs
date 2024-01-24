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
using InsternShip.Data.ViewModels.Certificate;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class CertificateRepository_UnitTest
    {
        private readonly CertificateRepository _CertificateRepository;
        private readonly CertificateService _CertificateService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICertificateRepository _fakeCertificateRepository = A.Fake<ICertificateRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public CertificateRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _CertificateRepository = new CertificateRepository(_fakeDbContext, _fakeUow, _mapper);
            _CertificateService = new CertificateService(_fakeCertificateRepository, _mapper);
        }

        [Fact]
        public async Task Add_Certificate_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeCertificateId = Guid.NewGuid();

            var fakeCreatedCertificate = new CertificateAddModel{
            };

            var mappedCreatedCertificate =_mapper.Map<CertificateModel>(fakeCreatedCertificate);
            mappedCreatedCertificate.CertificateId = fakeCertificateId;
            //Act
            var response = await _CertificateRepository.SaveCertificate(mappedCreatedCertificate);

            //Assert
            Assert.NotEqual(mappedCreatedCertificate.CertificateId, response.CertificateId);
        }   

        [Fact]
        public async Task Get_Certificate_Returns_Correctly(){
            
            //Arrange
            List<CertificateModel> list = new();
            var expectedCreatedCertificate1 = new CertificateModel{
                CertificateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedCertificate2 = new CertificateModel{
                CertificateId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedCertificate3 = new CertificateModel{
                CertificateId = Guid.NewGuid(),
                IsDeleted = false
            };
            list.Add(expectedCreatedCertificate1);
            list.Add(expectedCreatedCertificate2);
            list.Add(expectedCreatedCertificate3);

            //Act
            A.CallTo(() => _fakeCertificateRepository.GetAllCertificate(null)).Returns(list);
            var response = await _CertificateService.GetAllCertificate(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<CertificateViewModel>>(response);
        }
    }
}

