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
using InsternShip.Data.ViewModels.CategoryQuestion;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class CategoryQuestionRepository_UnitTest
    {
        private readonly CategoryQuestionRepository _CategoryQuestionRepository;
        private readonly CategoryQuestionService _CategoryQuestionService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly ICategoryQuestionRepository _fakeCategoryQuestionRepository = A.Fake<ICategoryQuestionRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public CategoryQuestionRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _CategoryQuestionRepository = new CategoryQuestionRepository(_fakeDbContext, _fakeUow, _mapper);
            _CategoryQuestionService = new CategoryQuestionService(_fakeCategoryQuestionRepository, _mapper);
        }

        [Fact]
        public async Task Add_CategoryQuestion_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeCategoryQuestionId = Guid.NewGuid();

            var fakeCreatedCategoryQuestion = new CategoryQuestionAddModel{
            };

            var mappedCreatedCategoryQuestion =_mapper.Map<CategoryQuestionModel>(fakeCreatedCategoryQuestion);
            mappedCreatedCategoryQuestion.CategoryQuestionId = fakeCategoryQuestionId;
            //Act
            var response = await _CategoryQuestionRepository.SaveCategoryQuestion(mappedCreatedCategoryQuestion);

            //Assert
            Assert.NotEqual(mappedCreatedCategoryQuestion.CategoryQuestionId, response.CategoryQuestionId);
        }   

        [Fact]
        public async Task Get_CategoryQuestion_Returns_Correctly(){
            
            //Arrange
            List<CategoryQuestionModel> list = new();
            var expectedCreatedCategoryQuestion1 = new CategoryQuestionModel{
                CategoryQuestionId = Guid.NewGuid(),
                
            };
            var expectedCreatedCategoryQuestion2 = new CategoryQuestionModel{
                CategoryQuestionId = Guid.NewGuid(),
                
            };
            var expectedCreatedCategoryQuestion3 = new CategoryQuestionModel{
                CategoryQuestionId = Guid.NewGuid(),
                
            };
            list.Add(expectedCreatedCategoryQuestion1);
            list.Add(expectedCreatedCategoryQuestion2);
            list.Add(expectedCreatedCategoryQuestion3);

            //Act
            A.CallTo(() => _fakeCategoryQuestionRepository.GetAllCategoryQuestions()).Returns(list);
            var response = await _CategoryQuestionService.GetAllCategoryQuestions();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<CategoryQuestionViewModel>>(response);
        }
    }
}

