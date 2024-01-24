using AutoMapper;
using InsternShip.Data.Entities;
using InsternShip.Data.Interfaces;
using InsternShip.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InsternShip.Data.Repositories
{
    public class RequirementRepository : Repository<Requirement>, IRequirementRepository
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RequirementRepository(RecruitmentWebContext context,
            IUnitOfWork uow,
            IMapper mapper) : base(context)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequirementModel>> GetAllRequirement()
        {
            var listData = new List<RequirementModel>();

            var data = await Entities.ToListAsync();
            foreach (var item in data)
            {
                var obj = _mapper.Map<RequirementModel>(item);
                listData.Add(obj);
            }
            return listData;
        }

        public async Task<RequirementModel> SaveRequirement(RequirementModel request)
        {
            var report = _mapper.Map<Requirement>(request);
            report.RequirementId = Guid.NewGuid();

            Entities.Add(report);
            _uow.SaveChanges();

            var response = _mapper.Map<RequirementModel>(report);
            return await Task.FromResult(response);
        }

        public async Task<bool> UpdateRequirement(RequirementModel request, Guid requestId)
        {
            var report = _mapper.Map<Requirement>(request);
            report.RequirementId = requestId;

            Entities.Update(report);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRequirement(Guid requestId)
        {
            var entity = await Entities.FirstOrDefaultAsync(x => x.RequirementId == requestId);
            if (entity is null or { IsDeleted: true })
            {
                return await Task.FromResult(false);
            }
            entity.IsDeleted = true;
            Entities.Update(entity);
            _uow.SaveChanges();

            return await Task.FromResult(true);
        }

        public async Task<List<RequirementModel>> GetRequirementsByPositionId(Guid positionId)
        {
            var requirementList = Entities.AsAsyncEnumerable();
            List<RequirementModel> result = new();
            await foreach (var requirement in requirementList)
            {
                if (requirement.PositionId == positionId)
                {
                    result.Add(_mapper.Map<RequirementModel>(requirement));
                }
            }
            return result;
        }
    }
}