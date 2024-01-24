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
using InsternShip.Data.ViewModels.Department;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class DepartmentRepository_UnitTest
    {
        private readonly DepartmentRepository _DepartmentRepository;
        private readonly DepartmentService _DepartmentService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IDepartmentRepository _fakeDepartmentRepository = A.Fake<IDepartmentRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public DepartmentRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _DepartmentRepository = new DepartmentRepository(_fakeDbContext, _fakeUow, _mapper);
            _DepartmentService = new DepartmentService(_fakeDepartmentRepository, _mapper);
        }

        [Fact]
        public async Task Add_Department_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeDepartmentId = Guid.NewGuid();

            var fakeCreatedDepartment = new DepartmentAddModel{
                DepartmentName = "string",
            };

            var mappedCreatedDepartment =_mapper.Map<DepartmentModel>(fakeCreatedDepartment);
            mappedCreatedDepartment.DepartmentId = fakeDepartmentId;
            //Act
            var response = await _DepartmentRepository.SaveDepartment(mappedCreatedDepartment);

            //Assert
            Assert.NotEqual(mappedCreatedDepartment.DepartmentId, response.DepartmentId);
        }   

        [Fact]
        public async Task Get_Department_Returns_Correctly(){
            
            //Arrange
            List<DepartmentModel> departmentList = new();
            var expectedCreatedDepartment1 = new DepartmentModel{
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "string",
                IsDeleted = false
            };
            var expectedCreatedDepartment2 = new DepartmentModel{
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "string",
                IsDeleted = false
            };
            var expectedCreatedDepartment3 = new DepartmentModel{
                DepartmentId = Guid.NewGuid(),
                DepartmentName = "string",
                IsDeleted = false
            };
            departmentList.Add(expectedCreatedDepartment1);
            departmentList.Add(expectedCreatedDepartment2);
            departmentList.Add(expectedCreatedDepartment3);
            //Act
            A.CallTo(() => _fakeDepartmentRepository.GetAllDepartment(null)).Returns(departmentList);
            var response = await _DepartmentService.GetAllDepartment(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<DepartmentViewModel>>(response);
        }
    }
}

