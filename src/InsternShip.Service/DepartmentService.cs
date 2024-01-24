using AutoMapper;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using InsternShip.Data.ViewModels.Department;
using InsternShip.Service.Interfaces;

namespace InsternShip.Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteDepartment(Guid requestId)
        {
            return await _departmentRepository.DeleteDepartment(requestId);
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartment(string? request)
        {
            var data = await _departmentRepository.GetAllDepartment(request);
            List<DepartmentViewModel> result = new List<DepartmentViewModel>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var obj = _mapper.Map<DepartmentViewModel>(item);
                    result.Add(obj);
                }
                return result;
            }
            return null;
        }

        public async Task<DepartmentViewModel> SaveDepartment(DepartmentAddModel request)
        {
            var data = _mapper.Map<DepartmentModel>(request);
            var response = await _departmentRepository.SaveDepartment(data);
            return _mapper.Map<DepartmentViewModel>(response);
        }

        public async Task<bool> UpdateDepartment(DepartmentUpdateModel request, Guid requestId)
        {
            var data = _mapper.Map<DepartmentModel>(request);
            return await _departmentRepository.UpdateDepartment(data, requestId);
        }
    }
}