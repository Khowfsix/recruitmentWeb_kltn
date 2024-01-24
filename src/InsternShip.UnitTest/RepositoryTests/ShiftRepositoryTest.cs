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
using InsternShip.Data.ViewModels.Shift;
using InsternShip.Data.Mapping;

namespace InsternShip.UnitTest.RepositoryTests
{
    public class ShiftRepository_UnitTest
    {
        private readonly ShiftRepository _ShiftRepository;
        private readonly ShiftService _ShiftService;

        private readonly RecruitmentWebContext _fakeDbContext = A.Fake<RecruitmentWebContext>();
        private readonly IShiftRepository _fakeShiftRepository = A.Fake<IShiftRepository>();
        private readonly IUnitOfWork _fakeUow = A.Fake<IUnitOfWork>();
        private readonly IMapper _mapper;
        public ShiftRepository_UnitTest(){
            _mapper = new MapperConfiguration(cfg => 
                        { 
                            cfg.AddProfile(new AutoMapperConfiguration());
                        }).CreateMapper();
                        
            _ShiftRepository = new ShiftRepository(_fakeDbContext, _fakeUow, _mapper);
            _ShiftService = new ShiftService(_fakeShiftRepository, _mapper);
        }

        [Fact]
        public async Task Add_Shift_In_Repository_Returns_Correctly()
        {
            //Code để check repo trả về model với id tạo ở model, tên giống tên truyền vào từ add model
            //Arrange
            var fakeShiftId = Guid.NewGuid();

            var fakeCreatedShift = new ShiftAddModel{
                
            };

            var mappedCreatedShift =_mapper.Map<ShiftModel>(fakeCreatedShift);
            mappedCreatedShift.ShiftId = fakeShiftId;
            //Act
            var response = await _ShiftRepository.SaveShift(mappedCreatedShift);
            //Assert
            Assert.NotEqual(mappedCreatedShift.ShiftId, response.ShiftId);
        }   

        [Fact]
        public async Task Get_Shift_Returns_Correctly(){
            
            //Arrange
            List<ShiftModel> list = new();
            var expectedCreatedShift1 = new ShiftModel{
                ShiftId = Guid.NewGuid(),
            };
            var expectedCreatedShift2 = new ShiftModel{
                ShiftId = Guid.NewGuid(),
            };
            var expectedCreatedShift3 = new ShiftModel{
                ShiftId = Guid.NewGuid(),
            };
            list.Add(expectedCreatedShift1);
            list.Add(expectedCreatedShift2);
            list.Add(expectedCreatedShift3);

            //Act
            A.CallTo(() => _fakeShiftRepository.GetAllShifts(null)).Returns(list);
            var response = await _ShiftService.GetAllShifts(null);

            //Assert
            Assert.IsAssignableFrom<IEnumerable<ShiftViewModel>>(response);
        }
    }
}

