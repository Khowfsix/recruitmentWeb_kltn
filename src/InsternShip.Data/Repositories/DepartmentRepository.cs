using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DepartmentRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<bool> DeleteDepartment(Guid requestId)
        {
            try
            {
                var department = GetById(requestId);
                if (department == null)
                    throw new ArgumentNullException(nameof(department));

                //Entities.Remove(department);
                department.IsDeleted = true;
                Entities.Update(department);

                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<DepartmentModel>> GetAllDepartment(string? request)
        {
            try
            {
                var listData = new List<DepartmentModel>();
                if (string.IsNullOrEmpty(request))
                {
                    var data = await Entities
                        .ToListAsync();
                    listData = _mapper.Map<List<DepartmentModel>>(data);
                }
                else
                {
                    var data = await Entities
                        .Where(rp => rp.DepartmentName.Contains(request))
                        .ToListAsync();
                    listData = _mapper.Map<List<DepartmentModel>>(data);
                }

                return listData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<DepartmentModel> SaveDepartment(DepartmentModel request)
        {
            try
            {
                var department = _mapper.Map<Department>(request);
                department.DepartmentId = Guid.NewGuid();

                Entities.Add(department);
                _uow.SaveChanges();

                var response = _mapper.Map<DepartmentModel>(department);
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateDepartment(DepartmentModel request, Guid requestId)
        {
            try
            {
                var department = _mapper.Map<Department>(request);
                department.DepartmentId = requestId;

                Entities.Update(department);
                _uow.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}