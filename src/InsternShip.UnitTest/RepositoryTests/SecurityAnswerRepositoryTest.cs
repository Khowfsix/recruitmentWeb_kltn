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
using InsternShip.Data.ViewModels.SecurityAnswer;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class SecurityAnswerRepository_UnitTest
    {
        private readonly SecurityAnswerRepository _SecurityAnswerRepository;
        private readonly SecurityAnswerService _SecurityAnswerService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ISecurityAnswerRepository _fakeSecurityAnswerRepository = A.Fake<ISecurityAnswerRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public SecurityAnswerRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _SecurityAnswerRepository = new SecurityAnswerRepository(_fakeDbContext, _fakeUow, _mapper);
            _SecurityAnswerService = new SecurityAnswerService(_fakeSecurityAnswerRepository, _mapper);
        }

        [Fact]
        public async Task Add_SecurityAnswer_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeSecurityAnswerId = Guid.NewGuid();

            var fakeCreatedSecurityAnswer = new SecurityAnswerAddModel{
            };

            var mappedCreatedSecurityAnswer =_mapper.Map<SecurityAnswerModel>(fakeCreatedSecurityAnswer);
            mappedCreatedSecurityAnswer.SecurityAnswerId = fakeSecurityAnswerId;
            //Act
            var response = await _SecurityAnswerRepository.SaveSecurityAnswer(mappedCreatedSecurityAnswer);

            //Assert
            Assert.NotEqual(mappedCreatedSecurityAnswer.SecurityAnswerId, response.SecurityAnswerId);
        }   

        [Fact]
        public async Task Get_SecurityAnswer_Returns_Correctly(){
            
            //Arrange
            List<SecurityAnswerModel> list = new();
            var expectedCreatedSecurityAnswer1 = new SecurityAnswerModel{
                SecurityAnswerId = Guid.NewGuid(),
                
            };
            var expectedCreatedSecurityAnswer2 = new SecurityAnswerModel{
                SecurityAnswerId = Guid.NewGuid(),
                
            };
            var expectedCreatedSecurityAnswer3 = new SecurityAnswerModel{
                SecurityAnswerId = Guid.NewGuid(),
                
            };
            list.Add(expectedCreatedSecurityAnswer1);
            list.Add(expectedCreatedSecurityAnswer2);
            list.Add(expectedCreatedSecurityAnswer3);

            //Act
            A.CallTo(() => _fakeSecurityAnswerRepository.GetAllSecurityAnswers()).Returns(list);
            var response = await _SecurityAnswerService.GetAllSecurityAnswers();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<SecurityAnswerViewModel>>(response);
        }
    }
}

