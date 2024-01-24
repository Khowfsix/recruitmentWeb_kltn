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
using InsternShip.Data.ViewModels.Report;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class ReportRepository_UnitTest
    {
        private readonly ReportRepository _ReportRepository;
        private readonly ReportService _ReportService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IInterviewRepository _fakeInterviewRepository = A.Fake<InterviewRepository>();
        private readonly IApplicationRepository _fakeApplicationRepository = A.Fake<IApplicationRepository>();
        private readonly IReportRepository _fakeReportRepository = A.Fake<IReportRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public ReportRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _ReportRepository = new ReportRepository(_fakeDbContext, _fakeUow, _mapper);
            _ReportService = new ReportService(_fakeApplicationRepository,
                    _fakeReportRepository, _mapper, _fakeInterviewRepository);
        }

        [Fact]
        public async Task Add_Report_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeReportId = Guid.NewGuid();

            var fakeCreatedReport = new ReportAddModel{
                ReportName = "string",
            };

            var mappedCreatedReport =_mapper.Map<ReportModel>(fakeCreatedReport);
            mappedCreatedReport.ReportId = fakeReportId;
            //Act
            var response = await _ReportRepository.SaveReport(mappedCreatedReport);

            //Assert
            Assert.NotEqual(mappedCreatedReport.ReportId, response.ReportId);
        }   

        [Fact]
        public async Task Get_Report_Returns_Correctly(){
            
            //Arrange
            List<ReportModel> list = new();
            var expectedCreatedReport1 = new ReportModel{
                ReportId = Guid.NewGuid(),
                ReportName = "string",
                IsDeleted = false
            };
            var expectedCreatedReport2 = new ReportModel{
                ReportId = Guid.NewGuid(),
                ReportName = "string",
                IsDeleted = false
            };
            var expectedCreatedReport3 = new ReportModel{
                ReportId = Guid.NewGuid(),
                ReportName = "string",
                IsDeleted = false
            };
            list.Add(expectedCreatedReport1);
            list.Add(expectedCreatedReport2);
            list.Add(expectedCreatedReport3);

            //Act
            A.CallTo(() => _fakeReportRepository.GetAllReport()).Returns(list);
            var response = await _ReportService.GetAllReport();

            //Assert
            Assert.IsAssignableFrom<IEnumerable<ReportViewModel>>(response);
        }
    }
}

