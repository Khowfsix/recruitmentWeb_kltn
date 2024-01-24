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
using InsternShip.Data.ViewModels.Result;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class ResultRepository_UnitTest
    {
        private readonly ResultRepository _ResultRepository;
        private readonly ResultService _ResultService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IResultRepository _fakeResultRepository = A.Fake<IResultRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public ResultRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _ResultRepository = new ResultRepository(_fakeDbContext, _fakeUow, _mapper);
            _ResultService = new ResultService(_fakeResultRepository, _mapper);
        }

        [Fact]
        public async Task Add_Result_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeResultId = Guid.NewGuid();

            var fakeCreatedResult = new ResultAddModel{
            };

            var mappedCreatedResult =_mapper.Map<ResultModel>(fakeCreatedResult);
            mappedCreatedResult.ResultId = fakeResultId;
            //Act
            var response = await _ResultRepository.SaveResult(mappedCreatedResult);

            //Assert
            Assert.NotEqual(mappedCreatedResult.ResultId, response.ResultId);
        }   

        [Fact]
        public async Task Get_Result_Returns_Correctly(){
            
            //Arrange
            List<ResultModel> list = new();
            var expectedCreatedResult1 = new ResultModel{
                ResultId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedResult2 = new ResultModel{
                ResultId = Guid.NewGuid(),
                IsDeleted = false
            };
            var expectedCreatedResult3 = new ResultModel{
                ResultId = Guid.NewGuid(),
                IsDeleted = false
            };
            list.Add(expectedCreatedResult1);
            list.Add(expectedCreatedResult2);
            list.Add(expectedCreatedResult3);

            //Act
            A.CallTo(() => _fakeResultRepository.GetAllResult()).Returns(list);
            var response = await _ResultService.GetAllResult();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<ResultViewModel>>(response);
        }
    }
}

