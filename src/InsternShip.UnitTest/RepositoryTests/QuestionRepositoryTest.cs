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
using InsternShip.Data.ViewModels.Question;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class QuestionRepository_UnitTest
    {
        private readonly QuestionRepository _QuestionRepository;
        private readonly QuestionService _QuestionService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICategoryQuestionRepository _fakeCategoryQuestionRepository = 
                                                    A.Fake<ICategoryQuestionRepository>();
        private readonly IQuestionRepository _fakeQuestionRepository = A.Fake<IQuestionRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public QuestionRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _QuestionRepository = new QuestionRepository(_fakeUow, _fakeDbContext, _mapper);
            _QuestionService = new QuestionService(_fakeQuestionRepository, _mapper, _fakeCategoryQuestionRepository);
        }

        [Fact]
        public async Task Add_Question_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeQuestionId = Guid.NewGuid();

            var fakeCreatedQuestion = new QuestionAddModel{
                QuestionString = "string",
            };

            var mappedCreatedQuestion =_mapper.Map<QuestionModel>(fakeCreatedQuestion);
            mappedCreatedQuestion.QuestionId = fakeQuestionId;
            //Act
            var response = await _QuestionRepository.AddQuestion(mappedCreatedQuestion);

            //Assert
            Assert.Equal(fakeCreatedQuestion.QuestionString, response.QuestionString);
            Assert.NotEqual(fakeQuestionId, response.QuestionId);
        }   

        [Fact]
        public async Task Get_Question_Returns_Correctly(){
            
            //Arrange
            var fakeQuestionId = Guid.NewGuid();
            var expectedCreatedQuestion = new QuestionModel{
                QuestionId = fakeQuestionId,
                QuestionString = "string",
            };

            //Act
            A.CallTo(() => _fakeQuestionRepository.GetQuestion(fakeQuestionId)).Returns(expectedCreatedQuestion);
            var response = await _QuestionService.GetQuestion(fakeQuestionId);

            //Assert
            Assert.Equal(expectedCreatedQuestion.QuestionId, response.QuestionId);
        }
    }
}

