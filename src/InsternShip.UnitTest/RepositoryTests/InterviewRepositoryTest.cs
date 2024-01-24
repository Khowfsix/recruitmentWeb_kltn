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
using InsternShip.Data.ViewModels.Interview;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class InterviewRepository_UnitTest
    {
        private readonly InterviewRepository _interviewRepository;
        private readonly InterviewService _interviewService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IRoundRepository _fakeRoundRepository = A.Fake<IRoundRepository>();
        private readonly IInterviewRepository _fakeInterviewRepository = A.Fake<IInterviewRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public InterviewRepository_UnitTest()
        {
            _mapper = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();

            _interviewRepository = new InterviewRepository(_fakeDbContext, _fakeUow, _mapper);
            _interviewService = new InterviewService(_fakeInterviewRepository, _fakeRoundRepository, null!, _mapper);
        }

        [Fact]
        public async Task Add_interview_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeInterviewId = Guid.NewGuid();

            var fakeCreatedInterview = new InterviewAddModel
            {
                InterviewerId = fakeInterviewId,
            };

            var mappedCreatedInterview = _mapper.Map<InterviewModel>(fakeCreatedInterview);
            //Act
            var response = await _interviewRepository.SaveInterview(mappedCreatedInterview);

            //Assert
            Assert.NotEqual(mappedCreatedInterview.InterviewId, response.InterviewId);
        }

        [Fact]
        public async Task Get_interview_Returns_Correctly()
        {

            //Arrange
            var fakeInterviewId = Guid.NewGuid();
            var expectedCreatedInterview = new InterviewModel
            {
                InterviewId = fakeInterviewId,
                IsDeleted = false
            };

            //Act
            A.CallTo(() => _fakeInterviewRepository.GetInterviewById(fakeInterviewId)).Returns(expectedCreatedInterview);
            var response = await _interviewService.GetInterviewById(fakeInterviewId);

            //Assert
            Assert.Equal(expectedCreatedInterview.InterviewId, response.InterviewId);
        }
    }
}

