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
using InsternShip.Data.ViewModels.CvHasSkill;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class CvHasSkillRepository_UnitTest
    {
        private readonly CvHasSkillRepository _CvHasSkillRepository;
        private readonly CvHasSkillService _CvHasSkillService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICvHasSkillrepository _fakeCvHasSkillRepository = A.Fake<ICvHasSkillrepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public  CvHasSkillRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _CvHasSkillRepository = new CvHasSkillRepository(_fakeDbContext, _fakeUow, _mapper);
            _CvHasSkillService = new CvHasSkillService(_fakeCvHasSkillRepository, _mapper);
        }

        [Fact]
        public async Task Add_CvHasSkill_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeCvId = Guid.NewGuid();

            var fakeCreatedCvHasSkill = new CvHasSkillAddModel{
                Cvid = fakeCvId,
            };

            var mappedCreatedCvHasSkill =_mapper.Map<CvHasSkillModel>(fakeCreatedCvHasSkill);
            mappedCreatedCvHasSkill.CvSkillsId = Guid.NewGuid();
            //Act
            var response = await _CvHasSkillRepository.SaveCvHasSkillService(mappedCreatedCvHasSkill);

            //Assert
            Assert.NotEqual(mappedCreatedCvHasSkill.CvSkillsId, response.CvSkillsId);
        }   

        [Fact]
        public async Task Get_CvHasSkill_Returns_Correctly(){
            
            //Arrange
            List<CvHasSkillModel> list = new();
            var expectedCreatedCvHasSkill1 = new CvHasSkillModel{
                CvSkillsId = Guid.NewGuid(),
            };
            var expectedCreatedCvHasSkill2 = new CvHasSkillModel{
                CvSkillsId = Guid.NewGuid(),
            };
            var expectedCreatedCvHasSkill3 = new CvHasSkillModel{
                CvSkillsId = Guid.NewGuid(),
            };
            list.Add(expectedCreatedCvHasSkill1);
            list.Add(expectedCreatedCvHasSkill2);
            list.Add(expectedCreatedCvHasSkill3);

            //Act
            A.CallTo(() => _fakeCvHasSkillRepository.GetAllCvHasSkillService(null)).Returns(list);
            var response = await _CvHasSkillService.GetAllCvHasSkillService(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<CvHasSkillViewModel>>(response);
        }
    }
}

