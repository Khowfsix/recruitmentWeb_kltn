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
using InsternShip.Data.ViewModels.Skill;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class SkillRepository_UnitTest
    {
        private readonly SkillRepository _SkillRepository;
        private readonly SkillService _SkillService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ISkillRepository _fakeSkillRepository = A.Fake<ISkillRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public SkillRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _SkillRepository = new SkillRepository(_fakeDbContext, _fakeUow, _mapper);
            _SkillService = new SkillService(_fakeSkillRepository, _mapper);
        }

        [Fact]
        public async Task Add_Skill_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeSkillId = Guid.NewGuid();

            var fakeCreatedSkill = new SkillAddModel{
                SkillName = "string",
            };

            var mappedCreatedSkill =_mapper.Map<SkillModel>(fakeCreatedSkill);
            mappedCreatedSkill.SkillId = fakeSkillId;
            //Act
            var response = await _SkillRepository.SaveSkill(mappedCreatedSkill);

            //Assert
            Assert.NotEqual(mappedCreatedSkill.SkillId, response.SkillId);
        }   

        [Fact]
        public async Task Get_Skill_Returns_Correctly(){
            
            //Arrange
            List<SkillModel> list = new();
            var expectedCreatedSkill1 = new SkillModel{
                SkillId = Guid.NewGuid(),
                SkillName = "string",
                IsDeleted = false
            };
            var expectedCreatedSkill2 = new SkillModel{
                SkillId = Guid.NewGuid(),
                SkillName = "string",
                IsDeleted = false
            };
            var expectedCreatedSkill3 = new SkillModel{
                SkillId = Guid.NewGuid(),
                SkillName = "string",
                IsDeleted = false
            };
            list.Add(expectedCreatedSkill1);
            list.Add(expectedCreatedSkill2);
            list.Add(expectedCreatedSkill3);

            //Act
            A.CallTo(() => _fakeSkillRepository.GetAllSkills(null)).Returns(list);
            var response = await _SkillService.GetAllSkills(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<SkillViewModel>>(response);
        }
    }
}

