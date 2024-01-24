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
using InsternShip.Data.ViewModels.Interviewer;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class InterviewerRepository_UnitTest
    {
        private readonly InterviewerRepository _InterviewerRepository;
        private readonly InterviewerService _InterviewerService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IInterviewerRepository _fakeInterviewerRepository = A.Fake<IInterviewerRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public InterviewerRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _InterviewerRepository = new InterviewerRepository(_fakeDbContext, _fakeUow, _mapper);
            _InterviewerService = new InterviewerService(_fakeInterviewerRepository, _mapper);
        }

        [Fact]
        public async Task Add_Interviewer_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeInterviewerId = Guid.NewGuid();

            var fakeCreatedInterviewer = new InterviewerAddModel{
                UserId = Guid.NewGuid().ToString(),
            };

            var mappedCreatedInterviewer =_mapper.Map<InterviewerModel>(fakeCreatedInterviewer);
            mappedCreatedInterviewer.InterviewerId = fakeInterviewerId;
            //Act
            var response = await _InterviewerRepository.SaveInterviewer(mappedCreatedInterviewer);

            //Assert
            Assert.NotEqual(mappedCreatedInterviewer.InterviewerId, response.InterviewerId);
        }   

        [Fact]
        public async Task Get_Interviewer_Returns_Correctly(){
            
            //Arrange
            var fakeInterviewerId = Guid.NewGuid();
            var expectedCreatedInterviewer = new InterviewerModel{
                InterviewerId = fakeInterviewerId,
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakeInterviewerRepository.GetInterviewerById(fakeInterviewerId)).Returns(expectedCreatedInterviewer);
            var response = await _InterviewerService.GetInterviewerById(fakeInterviewerId);

            //Assert
            Assert.Equal(expectedCreatedInterviewer.InterviewerId, response.InterviewerId);
        }
    }
}

